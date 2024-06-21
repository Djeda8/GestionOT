using DOU.GestionOT.BL.Dto;
using MediatR;

namespace DOU.GestionOT.API.Domain.Queries
{
    public class FindOtDtoByIdQuery : IRequest<OtDto>
    {
        public int Id { get; set; }
        // Add other properties as needed
    }
}
