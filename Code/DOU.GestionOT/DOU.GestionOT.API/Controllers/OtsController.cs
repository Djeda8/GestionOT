using AutoMapper;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using DOU.GestionOT.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtsController : ControllerBase
    {
        private readonly GestionOTContext _context;
        private readonly IMapper _mapper;

        public OtsController(GestionOTContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtDto>>> GetOt()
        {
            IQueryable<Ot> query = _context.Ot;

            var result = await _mapper.ProjectTo<OtDto>(query).ToListAsync();

            return result;
        }

        // GET: api/Ots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OtDto>> GetOt(int id)
        {
            //var ot = await _context.Ot.FindAsync(id);

            IQueryable<Ot> query = _context.Ot.Where(x => x.Id == id);

            var result = await _mapper.ProjectTo<OtDto>(query).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Ots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOt(int id, OtDto otdto)
        {
            if (id != otdto.Id)
            {
                return BadRequest();
            }

            Ot ot = _mapper.Map<Ot>(otdto);

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
        public async Task<ActionResult<OtDto>> PostOt(OtDto otdto)
        {
            Ot ot = _mapper.Map<Ot>(otdto);

            _context.Ot.Add(ot);
            await _context.SaveChangesAsync();

            OtDto otdtonew = _mapper.Map<OtDto>(ot);

            return CreatedAtAction("GetOt", new { id = otdtonew.Id }, otdtonew);
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
