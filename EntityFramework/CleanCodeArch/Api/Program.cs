using Database;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddServicesLayer();
builder.Services.AddDatabaseLayer();

var app = builder.Build();

app.MapControllers();

app.Run();