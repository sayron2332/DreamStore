using DreamStore.Core.Entites;
using DreamStore.Core.Entites.Order;
using DreamStore.Core.Entites.Product;
using DreamStore.Core.Entites.Token;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace DreamStore.Infrastructure.Context
{
    internal class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
            .HasIndex(r => r.Email)
            .IsUnique();
            builder.Entity<RefreshToken>()
           .HasIndex(r => r.Token)
           .IsUnique();
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RefreshToken> AppTokens { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppProductAttribute> AppProductAttributes { get; set; }
        public DbSet<AppProduct> AppProducts { get; set; }
        public DbSet<AppCategory> AppCategory { get; set; }
        public DbSet<AppOrderItem> AppOrderItems { get; set; }
        public DbSet<AppOrder> AppOrders { get; set; }
    }
}
