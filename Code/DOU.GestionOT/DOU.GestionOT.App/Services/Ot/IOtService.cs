using DOU.GestionOT.BL.Dto;

namespace DOU.GestionOT.App.Services.Ot
{
    public interface IOtService
    {
        Task<IEnumerable<OtDto>> GetOtsAsync();
    }
}