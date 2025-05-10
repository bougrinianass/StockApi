namespace AppGestionStockMVC.Models
{
    public class Commande
    {
        public int Id { get; set; }

        public string ClientId { get; set; } // Optionnel pour lien avec Identity
        public DateTime DateCommande { get; set; } = DateTime.UtcNow;
        public string Statut { get; set; } = "EnCours";

        public virtual ICollection<LigneCommande> LignesCommande { get; set; }
    }
}
