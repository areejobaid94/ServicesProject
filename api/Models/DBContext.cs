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
        public DbSet<UserServiceInterest> UserServiceInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserServiceInterest>()
            .HasOne(p => p.User)
            .WithMany(b => b.UserServiceInterests)
            .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<UserServiceInterest>()
            .HasOne(p => p.Service)
            .WithMany(b => b.UserServiceInterests)
            .HasForeignKey(p => p.ServiceId);

            modelBuilder.Entity<UserServiceInterest>()
            .HasOne(p => p.Interest)
            .WithMany(b => b.UserServiceInterests)
            .HasForeignKey(p => p.InterestId);
        }

    }
}