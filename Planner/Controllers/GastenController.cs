using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;

namespace Planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastenController : ControllerBase
    {
        private readonly PlannerContext _context;

        public GastenController(PlannerContext context)
        {
            _context = context;
        }

        // GET: api/Gasten
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gast>>> GetGast()
        {
            return await _context.Gast.ToListAsync();
        }

        // GET: api/Gasten/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gast>> GetGast(int id)
        {
            var gast = await _context.Gast.FindAsync(id);
            if (gast == null)
                return NotFound();
            return gast;
        }

        // PUT: api/Gasten/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGast(int id, Gast gast)
        {
            if (id != gast.Id)
                return BadRequest();
            if (!(_context.Gast?.Any(e => e.Id == id)).GetValueOrDefault())
                return NotFound();
            _context.Entry(gast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Gasten
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gast>> PostGast(Gast gast)
        {
            _context.Gast.Add(gast);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGast", new { id = gast.Id }, gast);
        }

        // DELETE: api/Gasten/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGast(int id)
        {
            var gast = await _context.Gast.FindAsync(id);
            if (gast == null)
                return NotFound();
            _context.Gast.Remove(gast);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
