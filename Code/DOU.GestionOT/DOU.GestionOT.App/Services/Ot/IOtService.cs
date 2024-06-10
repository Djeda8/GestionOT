using DOU.GestionOT.App.MVVM.Models.Ots;

namespace DOU.GestionOT.App.Services.Ot
{
    public interface IOtService
    {
        Task<List<OT>> GetOtsAsync();
    }
}