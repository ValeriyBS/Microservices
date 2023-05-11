using Items.MinimalApi.Client.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

var requireAuthenticatedUserPolicyV1 = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .RequireClaim("scope", "Warehouse.MinimalApi.Read")
    .Build();

var requireAuthenticatedUserPolicyV2 = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .RequireClaim("scope", "Warehouse.MinimalApi.Read")
    .Build();

app.MapGet("/warehouse/v1/items", async ([FromServices] IItemsClient itemsClient) =>

{
    var result = await itemsClient.GetItems();
    return Results.Ok(result);
}).RequireAuthorization(requireAuthenticatedUserPolicyV1);

app.MapGet("/warehouse/v2/items", async ([FromServices] IItemsClient itemsClient) =>

{
    var result = await itemsClient.GetItems();
    return Results.Ok(result);
}).RequireAuthorization(requireAuthenticatedUserPolicyV2);



app.MapGet("/warehouse/healthy", () => "warehouse beep");


app.Run();
