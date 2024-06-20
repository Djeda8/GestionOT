using DOU.GestionOT.BL.Dto;

namespace DOU.GestionOT.BL.Services
{
    public interface IOtBLService
    {
        Task<IEnumerable<OtDto>> GetOtsAsync();
        Task<OtDto> GetOtAsync(int? id);
        Task PutOtAsync(OtDto otdto);
        Task PostOtAsync(OtDto otdto);
        Task DeleteOtAsync(OtDto otdto);
    }
}
