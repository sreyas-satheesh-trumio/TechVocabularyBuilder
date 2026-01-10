using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<EndUser> EndUsers { get; set; }
    public DbSet<Topic> Topics { get; set; }   
    public DbSet<GameProgress> GameProgresses { get; set; }

    public DbSet<TopicLearned> TopicsLearned { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(t => t.TopicId);

            entity.Property(t => t.TopicName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(t => t.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(t => t.CreatedByUser)
                  .WithMany()
                  .HasForeignKey(t => t.CreatedBy)
                  .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<TopicLearned>(entity =>
    {
            entity.HasKey(tl => tl.TopicLearnedId);

            entity.Property(tl => tl.LearnedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(tl => tl.User)
                .WithMany()
                .HasForeignKey(tl => tl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tl => tl.Topic)
                .WithMany()
                .HasForeignKey(tl => tl.TopicId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent same topic being marked learned twice by same user
            entity.HasIndex(tl => new { tl.UserId, tl.TopicId })
                .IsUnique();
            });

        modelBuilder.Entity<GameProgress>(entity =>
        {   
            entity.HasKey(g => g.GameId);

            entity.Property(g => g.AttemptedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(g => g.User)
                .WithMany()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(g => g.Topic)
                .WithMany()
                .HasForeignKey(g => g.TopicId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent same user attempting same topic twice
            entity.HasIndex(g => new { g.UserId, g.TopicId })
                .IsUnique();
        });


        modelBuilder.Entity<EndUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.HasIndex(e => e.Username)
                  .IsUnique();

            entity.Property(e => e.Role)
                  .HasConversion<string>()
                  .HasDefaultValue(UserRole.User);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");
        });
    }
}
