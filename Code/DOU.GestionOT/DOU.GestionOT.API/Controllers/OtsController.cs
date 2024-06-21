using DOU.GestionOT.API.Domain.Commands;
using DOU.GestionOT.API.Domain.Queries;
using DOU.GestionOT.BL.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DOU.GestionOT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OtsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Ots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtDto>>> GetOts()
        {
            var ots = await _mediator.Send(new GetOtDtosQuery());
            return Ok(ots);
        }

        // GET: api/Ots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OtDto>> GetOt(int id)
        {
            var query = new FindOtDtoByIdQuery { Id = id };
            var book = await _mediator.Send(query);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
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

            var command = new UpdateOtDtoCommand(id, otdto.Estado, otdto.CodigoTipo, otdto.TipoParte, otdto.Ejercicio, otdto.Serie, otdto.Numero, otdto.Tipo, otdto.Cliente, otdto.Direccion, otdto.Fecha);
            await _mediator.Send(command);

            return NoContent();
        }

        // POST: api/Ots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OtDto>> PostOt(OtDto otdto)
        {
            var command = new CreateOtDtoCommand(otdto.Estado, otdto.CodigoTipo, otdto.TipoParte, otdto.Ejercicio, otdto.Serie, otdto.Numero, otdto.Tipo, otdto.Cliente, otdto.Direccion, otdto.Fecha);
            var otdtoNew = await _mediator.Send(command);
            return CreatedAtAction("GetOt", new { id = otdtoNew.Id }, otdtoNew);
        }

        // DELETE: api/Ots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOt(int id)
        {
            var command = new DeleteOtDtoCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
