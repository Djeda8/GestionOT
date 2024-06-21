using AutoMapper;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using DOU.GestionOT.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Domain.Queries
{
    public class FindOtDtoByIdHandler : IRequestHandler<FindOtDtoByIdQuery, OtDto>
    {
        private readonly GestionOTContext _context;
        private readonly IMapper _mapper;

        public FindOtDtoByIdHandler(GestionOTContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OtDto> Handle(FindOtDtoByIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Ot> query = _context.Ot.Where(x => x.Id == request.Id);

            var ot = await _mapper.ProjectTo<OtDto>(query).FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException($"Ot '{request.Id}' does not exist"); ;

            return ot;
        }
    }
}
