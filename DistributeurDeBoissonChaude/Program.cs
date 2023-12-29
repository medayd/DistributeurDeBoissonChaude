using DistributeurDeBoissonChaude.Api;
using DistributeurDeBoissonChaude.Api.DBContext;
using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;
using DistributeurDeBoissonChaude.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDistributeurService, DistributeurService>();
builder.Services.AddSingleton<IDistributeurRepository, DistributeurRepository>();
builder.Services.Configure<MargeConfig>(builder.Configuration.GetSection("MargeConfig"));
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
