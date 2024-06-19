using DOU.GestionOT.BL.Entities;
using DOU.GestionOT.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtsController : ControllerBase
    {
        private readonly GestionOTContext _context;

        public OtsController(GestionOTContext context)
        {
            _context = context;
        }

        // GET: api/Ots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ot>>> GetOt()
        {
            return await _context.Ot.ToListAsync();
        }

        // GET: api/Ots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ot>> GetOt(int id)
        {
            var ot = await _context.Ot.FindAsync(id);

            if (ot == null)
            {
                return NotFound();
            }

            return ot;
        }

        // PUT: api/Ots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOt(int id, Ot ot)
        {
            if (id != ot.Id)
            {
                return BadRequest();
            }

            _context.Entry(ot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ot>> PostOt(Ot ot)
        {
            _context.Ot.Add(ot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOt", new { id = ot.Id }, ot);
        }

        // DELETE: api/Ots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOt(int id)
        {
            var ot = await _context.Ot.FindAsync(id);
            if (ot == null)
            {
                return NotFound();
            }

            _context.Ot.Remove(ot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OtExists(int id)
        {
            return _context.Ot.Any(e => e.Id == id);
        }
    }
}
