﻿using AutoMapper;
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
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<OtDto>> Handle(GetOtDtosQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Ot> query = _context.Ot;

            var ots = await query.ToListAsync(cancellationToken);

            var result = _mapper.Map<IEnumerable<OtDto>>(ots);

            return result;
        }
    }
}
