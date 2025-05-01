using ApiStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStock.Data
{
    public class StockContext:DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options) { }
        public DbSet<Produit> Produits { get; set; }
    }
}
