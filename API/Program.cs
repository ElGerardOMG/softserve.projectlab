using API.Models.Entities;
using API.Data;
using API.implementations.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using API.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<ProjectlabContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
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

var conectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<ProjectlabContext>(
    db => db.UseSqlServer(conectionString));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.AllowAnyOrigin() 
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});



builder.Services.AddTransient<IProductProcessor, ProductProcessor>();

builder.Services.AddTransient<IUserProcessor, UserProcessor>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IEmailSender<User>, DummyEmailSender>();


builder.Services.AddAuthorization();
/*
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });

*/
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.MapIdentityApi<User>();

app.UseCors("AllowFrontend");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseStaticFiles(); // Serve files from wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")),
     RequestPath = "/images"
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();