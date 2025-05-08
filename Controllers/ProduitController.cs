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

        [HttpGet("getProduits")]
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

        // PUT: api/Produit/5
        [HttpPut("EditProduit")]
        public async Task<IActionResult> EditProduit(Produit produit)
        {

            _context.Entry(produit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitExists(produit.Id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent(); // Standard pour PUT
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.Id == id);
        }

    }


}
