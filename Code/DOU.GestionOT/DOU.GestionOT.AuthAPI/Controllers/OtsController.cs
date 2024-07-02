using AutoMapper;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using DOU.GestionOT.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class OtsController : ControllerBase
    {
        private readonly GestionOTContext _context;
        private readonly IMapper _mapper;

        public OtsController(GestionOTContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Ots
        [HttpGet]
        public async Task<IEnumerable<OtDto>> GetOts()
        {
            IQueryable<Ot> query = _context.Ot;

            var ots = await query.ToListAsync();

            var result = _mapper.Map<IEnumerable<OtDto>>(ots);

            return result;
        }
    }
}
