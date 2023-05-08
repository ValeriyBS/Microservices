using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApiGateway.WebApi.DelegatingHandlers;
using JwtAuthenticationManager;
using Ocelot.Values;

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

var authenticationScheme = "ApiGatewayAuthenticationScheme";

//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

//builder.Services.AddAuthentication(o =>
//    {
//        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false;
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = "https://localhost:5010",
//            ValidAudience = "Warehouse.MinimalApi",
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.ASCII.GetBytes("thisisthesecretforgeneratingakey(mustbeatleast32bitlong)"))
//        };
//    });

builder.Services.AddHttpClient();
builder.Services.AddScoped<TokenExchangeDelegatingHandler>();

builder.Services.AddOcelot()
    .AddDelegatingHandler<TokenExchangeDelegatingHandler>();
builder.Services.AddCustomJwtAuthentication(new List<string>{"apigateway"}, "https://localhost:5010");

var app = builder.Build();

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
