using Microsoft.EntityFrameworkCore;
using YDTest.Data.Entities;

namespace YDTest.Data;
public class YDTestContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public YDTestContext(DbContextOptions<YDTestContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
    }
}
