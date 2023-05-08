using Items.Contracts.Responses;
using Items.MinimalApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/items", () => new ItemsResponseDto
    {
        Name = "SomeItemName"
    })
    .AddEndpointFilter<ApiKeyAuthenticationEndpointFilter>();
app.MapGet("/healthy", () => "beep");
app.MapGet("/", () => "hello");

app.Run();