using ApacheKafkaConsumerDemo;
using Microsoft.EntityFrameworkCore;
using SA2Project.Controllers;
using SA2Project.Models;
using SA2Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Configuration of Connection string 
var conncetion = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>
    options.UseSqlServer(conncetion) );

// Add Offer Services
builder.Services.AddScoped<IOfferService, OfferService>();

builder.Services.AddHostedService<ApacheKafkaConsumerService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
