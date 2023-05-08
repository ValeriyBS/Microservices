using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<JwtTokenHandler>();

var app = builder.Build();

app.MapPost("/identity/exchangetoken", async (AuthenticationRequest request, JwtTokenHandler tokenHandler) =>
{
    var authenticationResponse = tokenHandler.GenerateJwtToken(request);
    return Results.Created("identity/exchangetoken", authenticationResponse);
});

app.MapGet("/identity/healthy", () => "identity beep");

app.Run();
