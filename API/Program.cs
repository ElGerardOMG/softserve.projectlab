using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using API.Entity;
using API.implementations.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.AllowAnyOrigin() 
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var conectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services.AddTransient<IProductProcessor, ProductProcessor>();

builder.Services.AddTransient<IUserProcessor, UserProcessor>();

builder.Services.AddDbContext<AppDbContext>(
    db => db.UseSqlServer(conectionString), ServiceLifetime.Singleton
    );

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseAuthorization();
app.MapControllers();
app.Run();