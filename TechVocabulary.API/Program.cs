
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.IdentityModel.Tokens;
 using System.Text; 
 using System.Security.Claims;
  using TechVocabulary.API.Models;
   using TechVocabulary.API.Services; 
   using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// 🔐 JWT (unchanged)
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection);
var jwtSettings = jwtSection.Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key)
            ),
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero
        };
    });

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
        policy
            .WithOrigins("http://localhost:5128")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// 🚨 DEV FIX: comment this temporarily
app.UseHttpsRedirection();

// ✅ CORS MUST COME FIRST
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
