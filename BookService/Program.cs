using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region JwtToken

var bindJwtSettings = new JwtSettings();
builder.Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
builder.Services.AddSingleton(bindJwtSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.Events = new JwtBearerEvents();
    options.Events.OnAuthenticationFailed = context =>
    {
        context.Response.OnStarting(async () =>
        {
            string response = (context.Exception.GetType() != typeof(SecurityTokenExpiredException)) ? "The access token provided is not valid." : "The access token provided has expired.";
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.NoResult();
                context.Response.StatusCode = StatusCodes.Status205ResetContent; //(int)HttpStatusCode.ResetContent;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("Token-Expired", "true");
                response = "The access token provided has expired.";
            }

            await context.Response.WriteAsync(response);
        });
        return Task.CompletedTask;
    };

    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
        ValidateIssuer = bindJwtSettings.ValidateIssuer,
        ValidIssuer = bindJwtSettings.ValidIssuer,
        ValidateAudience = bindJwtSettings.ValidateAudience,
        ValidAudience = bindJwtSettings.ValidAudience,
        ValidateLifetime = false
    };
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

