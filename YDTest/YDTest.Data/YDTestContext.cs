using Microsoft.EntityFrameworkCore;
using YDTest.Data.Entities;

namespace YDTest.Data;
public class YDTestContext : DbContext
{
    public DbSet<User> Users { get; set; }

    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<User>()
    //        .Property(u => u.DisplayName)
    //        .HasColumnName("display_name");
    //}
}
