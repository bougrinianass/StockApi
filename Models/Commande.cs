using ApiStock.Models;
using Microsoft.AspNetCore.Identity;


namespace AppGestionStockMVC.Models
{
    public class Commande
    {
        public int Id { get; set; }

        public string ClientId { get; set; }
        public virtual Client ?Client { get; set; }

        public DateTime DateCommande { get; set; }
        public string Statut { get; set; } // EnCours, Validée, Annulée...

        public virtual ICollection<LigneCommande> LignesCommande { get; set; }
    }
}
