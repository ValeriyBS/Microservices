using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CORSPolicy",
//        b => b.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
//});

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors("CORSPolicy ")app.UseHttpsRedirection();
app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
