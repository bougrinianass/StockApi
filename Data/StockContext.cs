using ApiStock.Models;
using AppGestionStockMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStock.Data
{
    public class StockContext:DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options) { }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<LigneCommande> LignesCommande { get; set; }

        public DbSet<Client> Clients { get; set; }

    }
}
