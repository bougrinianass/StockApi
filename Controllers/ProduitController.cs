using System.Net.Http.Headers;
using ApiStock.Data;
using ApiStock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStock.Controllers
{
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
            return await _context.Products.ToListAsync(); 
        }

        [HttpPost]
        public async Task<ActionResult<Produit>> AddProduct(Produit produit)
        {
            _context.Products.Add(produit);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getProduits), new { id = produit.Id },produit);
        }
    }
}
