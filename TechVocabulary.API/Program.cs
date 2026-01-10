using Microsoft.EntityFrameworkCore;
using TechVocabulary.API.Services;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Add services
// --------------------
builder.Services.AddControllers();

// Add EF Core DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Add your services
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ILearningService, LearningService>();

// Add Swagger for API testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// Configure CORS
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorDev", policy =>
    {
        policy.WithOrigins("http://localhost:5128") // frontend URL
              .AllowAnyHeader()                    // allow all headers
              .AllowAnyMethod();                   // allow GET, POST, DELETE, etc.
    });
});

var app = builder.Build();

// --------------------
// Middleware pipeline
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowBlazorDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
