using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<EndUser> EndUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EndUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.HasIndex(e => e.Username)
                  .IsUnique();

            // Store enum as string OR int (see note below)
            entity.Property(e => e.Role)
                  .HasConversion<string>()   // 👈 stores "User"/"Admin"
                  .HasDefaultValue(UserRole.User);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");
        });
    }
}
