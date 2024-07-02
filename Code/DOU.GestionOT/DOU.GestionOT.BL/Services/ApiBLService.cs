using System.Net.Http.Headers;

namespace DOU.GestionOT.BL.Services
{
    public class ApiBLService
    {
        protected HttpClient _client;

        public ApiBLService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
