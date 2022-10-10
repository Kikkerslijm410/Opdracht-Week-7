using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Models;
using Planner.Data;

namespace Planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveringenController : ControllerBase
    {
        private readonly PlannerContext _context;

        public ReserveringenController(PlannerContext context)
        {
            _context = context;
        }

        // GET: api/Reserveringen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservering>>> GetReservering()
        {
            return await _context.Reservering.ToListAsync();
        }

        // GET: api/Reserveringen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservering>> GetReservering(int id)
        {
            var reservering = await _context.Reservering.FindAsync(id);
            if (reservering == null)
                return NotFound();
            return reservering;
        }

        // PUT: api/Reserveringen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservering(int id, Reservering reservering)
        {
            if (id != reservering.Id)
                return BadRequest();
            if (!(_context.Reservering?.Any(e => e.Id == id)).GetValueOrDefault())
                return NotFound();
            _context.Entry(reservering).State = EntityState.Modified;
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

        // POST: api/Reserveringen
        [HttpPost]
        public async Task<ActionResult<Reservering>> PostReservering(Reservering reservering)
        {
            // (je kunt de volgende logica ook in een constraint op de database vastleggen,
            // zodat je hier niet ingewikkeld hoeft te doen met lock's)
            if (10 < await _context.Reservering.Where(r => r.Dag == reservering.Dag && r.DagDeel == reservering.DagDeel && r.Attractie.Id == reservering.Attractie.Id).CountAsync())
                return BadRequest(new { Message = "Maximaal 10 gasten kunnen tegelijkertijd in een attractie!" }); 
            _context.Reservering.Add(reservering);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetReservering", new { id = reservering.Id }, reservering);
        }

        // DELETE: api/Reserveringen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservering(int id)
        {
            var reservering = await _context.Reservering.FindAsync(id);
            if (reservering == null)
                return NotFound();
            _context.Reservering.Remove(reservering);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
