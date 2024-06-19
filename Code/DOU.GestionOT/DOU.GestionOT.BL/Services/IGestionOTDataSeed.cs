using DOU.GestionOT.BL.Entities;

namespace DOU.GestionOT.BL.Services
{
    public interface IGestionOTDataSeed
    {
        Ot[] GetInitialOts();
    }
}
