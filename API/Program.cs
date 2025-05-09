using Microsoft.EntityFrameworkCore;
using TavernSystem.Models;
using TavernSystem.Repositories;
using TavernSystem.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TavernDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IAdventurerRepository, AdventurerRepository>();
builder.Services.AddTransient<IAdventurerService, AdventurerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();