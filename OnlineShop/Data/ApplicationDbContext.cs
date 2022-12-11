using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductTypes> ProductTypes { get; set; }

        // method DbSet for SpecialTags
        public DbSet<SpecialTags> SpecialTags { get; set; }
        // method DbSet for Products
        public DbSet<Products> Products { get; set; }
    }
}