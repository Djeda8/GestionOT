using DOU.GestionOT.App.MVVM.Models.Ots;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.BL.Services;

namespace DOU.GestionOT.App.Services.Ot
{
    public class OtService : IOtService
    {
        private readonly IOtBLService _otBLService;
        public OtService(IOtBLService otBLService)
        {
            _otBLService = otBLService;
        }

        public async Task<IEnumerable<OtDto>> GetOtsAsync()
        {
            //Check Connectivity
            var current = Connectivity.NetworkAccess;
            IEnumerable<OtDto> itemList = null;

            if (current == NetworkAccess.Internet)
            {
                itemList = await _otBLService.GetOtsAsync();
            }
            else
            {
                //throw new Exception(AppResources.HttpConnectivityError);
            }

            return itemList;
        }
    }
}