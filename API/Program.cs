using API.Models.Entities;
using API.Data;
using API.implementations.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using API.Models.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.implementations.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var conectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<ProjectlabContext>(
    db => db.UseSqlServer(conectionString));

// TODO: Some endpoints must be protected for regular user access. Maybe adding additional roles, but
// that means we must change the DB

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<ProjectlabContext>()
    .AddDefaultTokenProviders();
/*
builder.Services.Configure<IdentityOptions>(options =>
{
    // TODO: Change these values for some more secure ones.

    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
*/





builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.AllowAnyOrigin() 
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddTransient<IProductProcessor, ProductProcessor>();
//builder.Services.AddTransient<IUserProcessor, UserProcessor>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IEmailSender<User>, DummyEmailSender>();
builder.Services.AddTransient<IUserAddressProcessor, UserAddressProcessor>();




builder.Services.AddControllers();

// Sirve para realizar la authenticación en el swagger
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Parámetros para la validación del token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, 
        ValidateAudience = true,
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 
        // Obtener los settings del token
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
    };
});
builder.Services.AddAuthorization();

// Agregar a la UI de Swagger la opción de authenticarte
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API",
        Description = "API Testing",
    });


    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization using Bearer Squeme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization", 
        In = ParameterLocation.Header, 
        Type = SecuritySchemeType.Http, 
        Scheme = "Bearer" 
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});

var app = builder.Build();

app.UseCors("AllowFrontend");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/images"
});

app.Run();