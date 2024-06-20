using DOU.GestionOT.DAL.Entities;

namespace DOU.GestionOT.BL.Services
{
    public interface IGestionOTDataSeed
    {
        Ot[] GetInitialOts();
    }
}
