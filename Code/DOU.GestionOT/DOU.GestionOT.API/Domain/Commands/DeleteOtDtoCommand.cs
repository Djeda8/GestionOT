using MediatR;

namespace DOU.GestionOT.API.Domain.Commands
{
    public class DeleteOtDtoCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteOtDtoCommand(int id)
        {
            Id = id;
        }
    }
}
