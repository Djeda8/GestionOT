using DOU.GestionOT.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DOU.GestionOT.API.Domain.Commands
{
    public class DeleteOtDtoHandler : IRequestHandler<DeleteOtDtoCommand>
    {
        private readonly GestionOTContext _context;

        public DeleteOtDtoHandler(GestionOTContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteOtDtoCommand command, CancellationToken cancellationToken)
        {
            var ot = await _context.Ot.SingleOrDefaultAsync((x => x.Id == command.Id), cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException($"Ot with id: '{command.Id}' does not exist");

            _context.Ot.Remove(ot);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
