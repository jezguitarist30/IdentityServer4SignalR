using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class SignalRContext: DbContext
    {
        public SignalRContext(DbContextOptions<SignalRContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Claim>().ToTable("Claim");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
    }
}
