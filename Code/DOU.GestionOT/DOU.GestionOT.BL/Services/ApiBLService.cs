using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
