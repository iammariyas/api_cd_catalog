using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using CDCatalog.Application;
using CDCatalog.Application.AddCD;
using CDCatalog.Application.DeleteCD;
using CDCatalog.Application.ViewersCatalog;
using CDCatalog.Application.ViewersCD;
using CDCatalog.Domain.Interfaces;
using CDCatalog.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("CDCatalog.Application")));
builder.Services.AddSingleton<ICDRepository, InMemoryCDRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();