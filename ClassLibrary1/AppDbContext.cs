using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace ClassLibrary1
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<CartEntity> Carts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var basePath = Path.GetFullPath(
                    Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\ClassLibrary1")
                    );
                var dbPath = Path.Combine(basePath, "appdata.db");

                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
                
        }
    }
}
