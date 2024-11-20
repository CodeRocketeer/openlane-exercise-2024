using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BiddingSystem.Infrastructure;
using BiddingSystem.Application;
using BiddingSystem.Infrastructure.Repositories;
using System;
using BiddingSystem.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQLite & DBContext
var solutionBasePath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName; // Get solution root
var infrastructurePath = Path.Combine(solutionBasePath, "BiddingSystem.Infrastructure", "data");
Directory.CreateDirectory(infrastructurePath); // Ensure the data folder exists
var dbPath = Path.Combine(infrastructurePath, "bidding.db"); builder.Services.AddDbContext<BiddingDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}",
        b => b.MigrationsAssembly("BiddingSystem.Infrastructure")));


// Configure MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PlaceBidCommandConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitHost = builder.Configuration.GetValue<string>("RabbitMQ:Host") ?? throw new InvalidOperationException("RabbitMQ Host is not configured.");
        var rabbitUsername = builder.Configuration.GetValue<string>("RabbitMQ:Username") ?? throw new InvalidOperationException("RabbitMQ Username is not configured.");
        var rabbitPassword = builder.Configuration.GetValue<string>("RabbitMQ:Password") ?? throw new InvalidOperationException("RabbitMQ Password is not configured.");

        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username(rabbitUsername);
            h.Password(rabbitPassword);
        });

        cfg.ReceiveEndpoint("bid-processing", e =>
        {
            e.ConfigureConsumer<PlaceBidCommandConsumer>(context);
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });
    });
});

// Register services
builder.Services.AddScoped<IBidRepository, BidRepository>();

var app = builder.Build();

// Use the GlobalExceptionMiddleware
app.UseMiddleware<GlobalExceptionMiddleware>();

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
