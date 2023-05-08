using Items.MinimalApi.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Warehouse.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

//var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .RequireClaim("city","City")
//    //.RequireClaim("given_name", "FirstName")
//    .Build();


app.MapGet("/warehouse/v1/items", async ([FromServices] IItemsClient itemsClient) =>

{
    var result = await itemsClient.GetItems();
    return Results.Ok(result);
});

app.MapGet("/warehouse/v2/items", async ([FromServices] IItemsClient itemsClient) =>

{
    var result = await itemsClient.GetItems();
    return Results.Ok(result);
}).RequireAuthorization();



app.MapGet("/warehouse/healthy", () => "warehouse beep");


app.Run();
