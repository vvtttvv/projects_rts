using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
// blog app users comments posts nested comments, nu poate edita poste diferite async + unit tests

/*
To do:
    - Unit Tests
    - Nested comments
    - Taking care of edditing not owned posts 

*/