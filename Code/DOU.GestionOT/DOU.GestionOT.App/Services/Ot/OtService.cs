using DOU.GestionOT.App.MVVM.Models.Ots;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOU.GestionOT.App.Services.Ot
{
    public class OtService : ApiService, IOtService
    {
        public async Task<List<OT>> GetOtsAsync()
        {
            var uri = new Uri($"{GlobalSettings.URL}/Ots");

            List<OT> itemList = null;

            //Check Connectivity
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var response = await _client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        var returnMessage = await response.Content.ReadAsStringAsync();

                        itemList = JsonConvert.DeserializeObject<List<OT>>(returnMessage);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                //throw new Exception(AppResources.HttpConnectivityError);
            }
            return itemList;
        }
    }
}