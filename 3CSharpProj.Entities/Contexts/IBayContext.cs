using Microsoft.EntityFrameworkCore;
using _3CSharpProj.Entities.Models;

namespace _3CSharpProj.Entities.Contexts
{
    public class IBayContext : DbContext
    {
        public IBayContext(DbContextOptions<IBayContext> options) : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        
    }
    
}
