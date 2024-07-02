using DOU.GestionOT.App.MVVM.Models.Login;
using DOU.GestionOT.BL.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace DOU.GestionOT.App.Services.Ot
{
    public class OtAuthService : IOtService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public OtAuthService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<OtDto>> GetOtsAsync()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null) return null!;

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken!;

            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                var result = await httpClient.GetFromJsonAsync<IEnumerable<OtDto>>("/api/Ots");
                return result;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
            return null;
        }
    }
}
