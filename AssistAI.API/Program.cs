using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Read Key valut URI from appsettings
var keyValutUri = builder.Configuration["AzureKeyValut:VaultUri"];

// Configure Key Valut integration
if (!string.IsNullOrEmpty(keyValutUri))
{
    builder.Configuration.AddAzureKeyVault(new Uri(keyValutUri), new DefaultAzureCredential());
}

// Add Authentication services using Azure AD
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// public endpoint - no authentication required
app.MapGet("/", () => Results.Ok("Welcome to AssistAI API"));

// SECURE ENDPOINTS
app.MapGet("/secure-data", [Authorize] () =>
{
    var clientSecret = builder.Configuration["AssistAI-Client-Secret"];
    if (string.IsNullOrEmpty(clientSecret))
    {
        return Results.Problem("Failed to retrieve secret from Azure Key Valult.");
    }
    return Results.Ok(new { secret = clientSecret });
});

app.Run();
