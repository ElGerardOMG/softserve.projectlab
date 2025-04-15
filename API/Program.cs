using API.Data;
using API.implementations.Domain;
using API.implementations.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var conectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services.AddTransient<IProductDomain, ProductDomain>();
builder.Services.AddTransient<IAttributeDomain, AttributeDomain>();
builder.Services.AddTransient<IAttributeCategoryDomain, AttributeCategoryDomain>();
builder.Services.AddTransient<IProductCategoryDomain, ProductCategoryDomain>();
builder.Services.AddTransient<IProductVariantDomain, ProductVariantDomain>();

builder.Services.AddDbContext<ProjectlabContext>(
    db => db.UseSqlServer(conectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();