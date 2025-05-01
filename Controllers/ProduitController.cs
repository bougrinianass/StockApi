using System.Net.Http.Headers;
using ApiStock.Data;
using ApiStock.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStock.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private readonly StockContext _context;

        public ProduitController(StockContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> getProduits()
        {
            return await _context.Produits.ToListAsync(); 
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<Produit>> AddProduct(Produit produit)
        {
            _context.Produits.Add(produit);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getProduits), new { id = produit.Id },produit);
        }
    }
}
