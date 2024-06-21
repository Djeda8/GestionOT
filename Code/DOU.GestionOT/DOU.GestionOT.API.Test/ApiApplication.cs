using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DOU.GestionOT.API.Test
{
    class ApiApplication : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _serviceOverride;

        public ApiApplication(Action<IServiceCollection> serviceOverride)
        {
            _serviceOverride = serviceOverride;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Add mock/test services to the builder here
            builder.ConfigureServices(_serviceOverride);

            return base.CreateHost(builder);
        }
    }
}
