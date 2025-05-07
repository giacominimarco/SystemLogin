using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoLogin.API.Models;

namespace ProjetoLogin.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<User>();
            var admin = new User
            {
                Id = 1,
                Username = "admin",
            };
            admin.Password = hasher.HashPassword(admin, "1234");

            var user = new User
            {
                Id = 2,
                Username = "user",
            };
            user.Password = hasher.HashPassword(user, "abcd");

            modelBuilder.Entity<User>().HasData(admin, user);
        }
    }
}