using Items.MinimalApi.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Warehouse.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/warehouse/v1/items", async ([FromServices]IItemsClient itemsClient) =>

{
    var result = await itemsClient.GetItems();
    return Results.Ok(result);
});
//app.MapGet("/items", () => "hi");


app.Run();
