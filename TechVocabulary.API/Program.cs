using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Add services
// --------------------

// Add Controllers
builder.Services.AddControllers();

// Add DbContext (LocalDB)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Optional: Swagger (recommended for API testing)
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
