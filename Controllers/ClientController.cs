using ApiStock.Data;
using ApiStock.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStock.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly StockContext _context;

        public ClientController(StockContext context)
        {
            _context = context;
        }

        [HttpGet("getClients")]
        public async Task<ActionResult<IEnumerable<Client>>> getClients()
        {
            return await _context.Clients.ToListAsync();
        }

        [HttpPost("AddClient")]
        public async Task<ActionResult<Produit>> AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getClients), new { id = client.Id }, client);
        }

        // PUT: api/Client/5
        [HttpPut("EditClient")]
        public async Task<IActionResult> EditClient(Client client)
        {

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.Id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent(); // Standard pour PUT
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }


    }
}
