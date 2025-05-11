using ApiStock.Data;
using ApiStock.Models;
using AppGestionStockMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : Controller
    {
        private readonly StockContext _stockContext;

        public CommandeController(StockContext stockContext)
        {
            _stockContext = stockContext;
        }


        [HttpGet("getCommandes")]
        public async Task<ActionResult<IEnumerable<Commande>>> getCommandes()
        {
            return await _stockContext.Commandes.Include(c=>c.LignesCommande).ThenInclude(lc=>lc.Produit).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Commande>> GetCommande(int id)
        {
            var commande = await _stockContext.Commandes
                .Include(c => c.LignesCommande)
                .ThenInclude(lc => lc.Produit)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (commande == null)
            {
                return NotFound();
            }

            return commande;
        }


        [HttpPost("AddCommande")]
        public async Task<ActionResult<Commande>> AddCommande(Commande commande)
        {
            _stockContext.Commandes.Add(commande);
            _stockContext.SaveChanges();
            return CreatedAtAction(nameof(getCommandes), new { id = commande.Id }, commande);
        }
    }
}
