using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DOU.GestionOT.App.Services
{
    public class ApiService
    {
        protected HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
