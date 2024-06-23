using api.Data;
using API.Interfaces;
using API.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
string connection = connectionString ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");
MySqlServerVersion version = new("8.0.21");

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connection, version));

builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
