using DOU.GestionOT.BL.Entities;

namespace DOU.GestionOT.BL.Services
{
    public interface IOtBLService
    {
        Task<IEnumerable<Ot>> GetOtsAsyncs();
    }
}
