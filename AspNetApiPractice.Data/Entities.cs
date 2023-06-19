using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetApiPractice.Data
{
    public class Entities : IdentityDbContext<AppUser>
    {
        public Entities(DbContextOptions<Entities> options) : base (options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WishlistsProducts> WishlistsProducts { get; set; }
    }
}
