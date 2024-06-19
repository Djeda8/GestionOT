using DOU.GestionOT.BL.Entities;
using Newtonsoft.Json;


namespace DOU.GestionOT.BL.Services
{
    public class OtBLService : ApiBLService, IOtBLService
    {
        public async Task<IEnumerable<Ot>> GetOtsAsyncs()
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Ots");

            List<Ot> itemList = null;

            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var returnMessage = await response.Content.ReadAsStringAsync();

                    itemList = JsonConvert.DeserializeObject<List<Ot>>(returnMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return itemList;
        }
    }
}
