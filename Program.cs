using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using ToDoListApi.Contexts;
using ToDoListApi.SetRepositories.IRepositories;
using ToDoListApi.SetRepositories.Repositories;
using ToDoListApi.SetUnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API for system of sign-in with Swagger and PostgreSQL",
        Contact = new OpenApiContact
        {
            Name = "Anderson",
            Email = "anderson.c.rms2005@gmail.com"
        }
    });
    //c.AddServer(new OpenApiServer { Url = "http://localhost:5252" });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITasksRepository, TaskRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
