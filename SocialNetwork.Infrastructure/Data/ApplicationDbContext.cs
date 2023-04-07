using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Entities.Chat;

namespace SocialNetwork.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		:base(options)
	{

	}

    public DbSet<User> Users { get; set; }
    public DbSet<UserPost> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }
    public DbSet<GroupSubscription> GroupSubscriptions { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
