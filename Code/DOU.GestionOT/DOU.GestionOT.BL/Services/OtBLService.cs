using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL.Entities;
using Newtonsoft.Json;
using System.Text;


namespace DOU.GestionOT.BL.Services
{
    public class OtBLService : ApiBLService, IOtBLService
    {
        public async Task<IEnumerable<OtDto>> GetOtsAsync()
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Ots");

            List<OtDto> itemList = null;

            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var returnMessage = await response.Content.ReadAsStringAsync();

                    itemList = JsonConvert.DeserializeObject<List<OtDto>>(returnMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return itemList;
        }

        public async Task<OtDto> GetOtAsync(int? id)
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Ots/{id}");// GET: api/Ots/5

            OtDto otDto = null;

            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var returnMessage = await response.Content.ReadAsStringAsync();

                    otDto = JsonConvert.DeserializeObject<OtDto>(returnMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return otDto;
        }

        public async Task PutOtAsync(OtDto otdto)
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Ots/{otdto.Id}"); // PUT: api / Ots / 5

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(otdto), Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(uri, content);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task PostOtAsync(OtDto otdto)
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Ots"); // POST: api / Ots

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(otdto), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, content);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteOtAsync(OtDto otdto)
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Ots/{otdto.Id}"); // DELETE: api / Ots / 5

            try
            {
                var response = await _client.DeleteAsync(uri);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
