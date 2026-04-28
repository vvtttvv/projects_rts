using BlogApp.API.Endpoints;
using BlogApp.API.Extensions;
using BlogApp.Postgres;
using BlogApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicesLayer();
builder.Services.AddDatabaseLayer(builder.Configuration);
builder.Services.AddApiDocumentation();

var app = builder.Build();

await app.ApplyMigrationsAndSeedingAsync();

app.UseHttpsRedirection();
app.UseApiDocumentation();

var group = app.MapGroup("/api");

group.MapCommentsEndpoints();
group.MapPostsEndpoints();
group.MapUsersEndpoints();

app.Run();
