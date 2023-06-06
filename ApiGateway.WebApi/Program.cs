using ApiGateway.WebApi.DelegatingHandlers;
using ApplicationInsights.Nuget.Extensions;
using JwtAuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
       b => b.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsights(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddScoped<TokenExchangeDelegatingHandler>();

builder.Services.AddOcelot()
    .AddDelegatingHandler<TokenExchangeDelegatingHandler>();

builder.Services.AddCustomJwtAuthentication(builder.Configuration);
//builder.Services.AddAuthentication().AddJwtBearer();

var app = builder.Build();
app.MapGet("/", () => "beep");
//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORSPolicy ");
app.UseHttpsRedirection();
app.UseOcelot().Wait();

app.Run();
