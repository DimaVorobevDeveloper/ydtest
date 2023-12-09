using Microsoft.EntityFrameworkCore;
using YDTest.Data.Entities;

namespace YDTest.Data;

public class YDTestContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<UserToken> UserTokens { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<UserTeam> UserTeams { get; set; }

    public YDTestContext(DbContextOptions<YDTestContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<UserToken>().ToTable("UserTokens");
        modelBuilder.Entity<Team>().ToTable("Teams");
        modelBuilder.Entity<UserTeam>().ToTable("UserTeams");
        //modelBuilder.Entity<UserTeam>()
        //    .HasMany(e => e.User)
        //    .WithMany(e => e.Team);
    }
}
