using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }

    // Added parameterless constructor
    public AppContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=ForumDb.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.AuthorId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId);

        modelBuilder.Entity<UserSubscription>()
            .HasOne(us => us.User)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserSubscription>()
            .HasOne(us => us.SubscribedToUser)
            .WithMany()
            .HasForeignKey(us => us.SubscribedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Add more configurations as needed for your entities
    }
}