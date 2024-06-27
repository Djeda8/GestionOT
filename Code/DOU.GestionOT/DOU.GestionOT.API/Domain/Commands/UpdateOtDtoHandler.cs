using DOU.GestionOT.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Domain.Commands
{
    public class UpdateOtDtoHandler : IRequestHandler<UpdateOtDtoCommand>
    {
        private readonly GestionOTContext _context;

        public UpdateOtDtoHandler(GestionOTContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateOtDtoCommand command, CancellationToken cancellationToken)
        {
            var ot = await _context.Ot.SingleOrDefaultAsync((x => x.Id == command.Id), cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException($"Ot with id: '{command.Id}' does not exist");

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

            _context.Entry(ot).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}