using Microsoft.EntityFrameworkCore;
namespace api.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<UserSeviceInterest> UserSeviceInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSeviceInterest>()
            .HasOne(p => p.User)
            .WithMany(b => b.UserSeviceInterests)
            .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<UserSeviceInterest>()
            .HasOne(p => p.Service)
            .WithMany(b => b.UserSeviceInterests)
            .HasForeignKey(p => p.ServiceId);

            modelBuilder.Entity<UserSeviceInterest>()
            .HasOne(p => p.Interest)
            .WithMany(b => b.UserSeviceInterests)
            .HasForeignKey(p => p.InterestId);
        }

    }
}