using DOU.GestionOT.BL.Dto;
using MediatR;

namespace DOU.GestionOT.API.Domain.Queries
{
    public class GetOtDtosQuery() : IRequest<IEnumerable<OtDto>>
    {
    }
}
