using AirlineBookingSystem.Persistence;
using DotNetEnv;
using System.IO;
using AirlineBookingSystem.Application;
using AirlineBookingSystem.Infrastructure;

// Load environment variables from .env file in current directory
Env.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddEnvironmentVariables();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
