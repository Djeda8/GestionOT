using AutoMapper;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using DOU.GestionOT.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Domain.Queries
{
    public class GetOtDtosHandler : IRequestHandler<GetOtDtosQuery, IEnumerable<OtDto>>
    {
        private readonly GestionOTContext _context;
        private readonly IMapper _mapper;

        public GetOtDtosHandler(GestionOTContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OtDto>> Handle(GetOtDtosQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Ot> query = _context.Ot;

            var result = await _mapper.ProjectTo<OtDto>(query).ToListAsync(cancellationToken);

            return result;
        }
    }
}
