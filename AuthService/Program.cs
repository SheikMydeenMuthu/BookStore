using AuthService.Infrastructure;
using AuthService.Infrastructure.Interfaces;
using AuthService.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var bindJwtSettings = new JwtSettings();
builder.Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
builder.Services.AddSingleton(bindJwtSettings);
builder.Services.AddTransient<IJwtAuthenticationManager, JwtAuthenticationManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

