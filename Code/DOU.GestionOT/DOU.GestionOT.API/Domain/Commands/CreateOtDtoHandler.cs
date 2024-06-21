using AutoMapper;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using DOU.GestionOT.DAL.Entities;
using MediatR;

namespace DOU.GestionOT.API.Domain.Commands
{
    public class CreateOtDtoHandler : IRequestHandler<CreateOtDtoCommand, OtDto>
    {
        private readonly GestionOTContext _context;
        private readonly IMapper _mapper;

        public CreateOtDtoHandler(GestionOTContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OtDto> Handle(CreateOtDtoCommand command, CancellationToken cancellationToken)
        {
            Ot ot = new Ot();

            ot.Estado = command.Estado;
            ot.CodigoTipo = command.CodigoTipo;
            ot.TipoParte = command.TipoParte;
            ot.Ejercicio = command.Ejercicio;
            ot.Serie = command.Serie;
            ot.Numero = command.Numero;
            ot.Tipo = command.Tipo;
            ot.Cliente = command.Cliente;
            ot.Direccion = command.Direccion;
            ot.Fecha = command.Fecha;

            _context.Ot.Add(ot);
            await _context.SaveChangesAsync(cancellationToken);

            OtDto otdtoNew = _mapper.Map<OtDto>(ot);
            return otdtoNew;
        }
    }
}
