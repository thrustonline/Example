using Microsoft.EntityFrameworkCore;

namespace TB_ShoppingCartAPI.Models
{
    public class InventoryContext : DbContext
    {
        public DbSet<Item> Items { get; set; }


        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
