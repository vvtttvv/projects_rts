using Microsoft.EntityFrameworkCore;
using Store.PostgreSQL;
using Store.PostgreSQL.Seed;
using Store.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddServicesLayer();
builder.Services.AddDatabaseLayer(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await dbContext.Database.MigrateAsync();
	await DbSeeder.SeedAsync(dbContext);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
// blog app users comments posts nested comments, nu poate edita poste diferite async + unit tests