using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/login", (string username, string role) =>
{
    if (string.IsNullOrWhiteSpace(username))
    {
        return Results.BadRequest("Invalid Username");
    }

    if (string.IsNullOrWhiteSpace(role))
    {
        return Results.BadRequest("Invalid role");
    }

    if (!Auth.Roles.AllRoles.Contains(role))
    {
        return Results.BadRequest($"{role} is currently unsupported");
    }

    var claimsPrincipal = new ClaimsPrincipal(
      new ClaimsIdentity(
        [new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, role)],
        BearerTokenDefaults.AuthenticationScheme
      )
    );
    return Results.SignIn(claimsPrincipal);
})
    .WithName("LoginUserWithRole")
    .WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
