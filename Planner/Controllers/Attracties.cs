using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;

namespace Planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractiesController : ControllerBase
    {
        private readonly PlannerContext _context;

        public AttractiesController(PlannerContext context)
        {
            _context = context;
        }

        // GET: api/Attracties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attractie>>> GetAttractie()
        {
            return await _context.Attractie.ToListAsync();
        }

        // GET: api/Attracties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attractie>> GetAttractie(int id)
        {
            var attractie = await _context.Attractie.FindAsync(id);
            if (attractie == null)
                return NotFound();
            return attractie;
        }

        // POST: api/Attracties
        [HttpPost]
        public async Task<ActionResult<Attractie>> PostAttractie(Attractie attractie)
        {
            _context.Attractie.Add(attractie);
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException e) {
                if (e.InnerException is SqliteException && ((SqliteException)e.InnerException).SqliteErrorCode == 19) 
                    return new JsonResult(new { Message = "Kan niet" }){ StatusCode = StatusCodes.Status403Forbidden };
                // Als de constraint gechecked zou worden in de programma-code (i.p.v. in de database), 
                // dan had de response gedetailleerder kunnen zijn: 
                // return new JsonResult(new { Message = "Attractie met die naam bestaat al!" }){ StatusCode = StatusCodes.Status403Forbidden }; 
                throw;
            }
            return CreatedAtAction("GetAttractie", new { id = attractie.Id }, attractie);
        }

        // DELETE: api/Attracties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttractie(int id)
        {
            var attractie = await _context.Attractie.FindAsync(id);
            if (attractie == null)
                return NotFound();
            _context.Attractie.Remove(attractie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
