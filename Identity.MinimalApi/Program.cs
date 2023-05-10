using Identity.MinimalApi;
using Identity.MinimalApi.Interfaces;
using JwtAuthenticationManager.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.MapPost("/identity/exchangetoken", async (AuthenticationRequest request, IExchangeToken exchangeToken) =>
{
    var authenticationResponse = await exchangeToken.Execute(request);
    return Results.Created("identity/exchangetoken", authenticationResponse);
});

app.MapGet("/identity/healthy", () => "identity beep");

app.Run();
