using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    // ------------------------------
    // DbSets
    // ------------------------------
    public DbSet<EndUser> EndUsers { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<TopicLearned> TopicsLearned { get; set; }
    public DbSet<GameProgress> GameProgresses { get; set; }

    // ------------------------------
    // Model configuration
    // ------------------------------
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ------------------------------
        // EndUser
        // ------------------------------
        modelBuilder.Entity<EndUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.HasIndex(e => e.Username)
                  .IsUnique();

            // Store Role enum as string
            entity.Property(e => e.Role)
                  .HasConversion<string>()
                  .HasDefaultValue(UserRole.User);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");
        });

        // ------------------------------
        // Topic
        // ------------------------------
        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(t => t.TopicId);

            entity.Property(t => t.TopicName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(t => t.Definition)
                  .IsRequired();

            entity.Property(t => t.RealWorldUsage)
                  .IsRequired();

            entity.Property(t => t.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");

            // Relationship → CreatedBy (EndUser)
            entity.HasOne(t => t.CreatedByUser)
                  .WithMany()
                  .HasForeignKey(t => t.CreatedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Admin deletion won't delete topics
        });

        // ------------------------------
        // TopicLearned
        // ------------------------------
        modelBuilder.Entity<TopicLearned>(entity =>
        {
            entity.HasKey(tl => tl.TopicLearnedId);

            entity.Property(tl => tl.LearnedAt)
                  .HasDefaultValueSql("GETDATE()");

            // Relationships
            entity.HasOne(tl => tl.User)
                  .WithMany()
                  .HasForeignKey(tl => tl.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tl => tl.Topic)
                  .WithMany()
                  .HasForeignKey(tl => tl.TopicId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ------------------------------
        // GameProgress
        // ------------------------------
        modelBuilder.Entity<GameProgress>(entity =>
        {
            entity.HasKey(gp => gp.GameId);

            entity.Property(gp => gp.Score)
                  .IsRequired();

            entity.Property(gp => gp.AttemptedAt)
                  .HasDefaultValueSql("GETDATE()");

            // Relationships
            entity.HasOne(gp => gp.User)
                  .WithMany()
                  .HasForeignKey(gp => gp.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(gp => gp.Topic)
                  .WithMany()
                  .HasForeignKey(gp => gp.TopicId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

