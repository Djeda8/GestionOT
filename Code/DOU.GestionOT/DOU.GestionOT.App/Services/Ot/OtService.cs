using DOU.GestionOT.App.MVVM.Models.Ots;
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

        public async Task<IEnumerable<OT>> GetOtsAsync()
        {
            //Check Connectivity
            var current = Connectivity.NetworkAccess;
            IEnumerable<OT> itemList = null;

            if (current == NetworkAccess.Internet)
            {
                //var uri = new Uri($"{GlobalSettings.URL}/Ots");

                var a = await _otBLService.GetOtsAsyncs();
                itemList = a.Select(x => new OT
                {
                    Id = x.Id,
                    Cliente = x.Cliente,
                    CodigoTipo = x.CodigoTipo,
                    Direccion = x?.Direccion,
                    Ejercicio = x.Ejercicio,
                    Serie = x.Serie,
                    Estado = x.Estado,
                    Fecha = x.Fecha,
                    Numero = x.Numero,
                    Tipo = x.Tipo,
                    TipoParte = x.TipoParte
                }
                );

            }
            else
            {
                //throw new Exception(AppResources.HttpConnectivityError);
            }

            return itemList;
        }
    }
}