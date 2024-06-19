using DOU.GestionOT.BL.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GestionOTServiceCollectionExtensions
    {
        public static void AddGestionOTCore(this IServiceCollection services)
        {
            services.AddScoped<IOtBLService, OtBLService>();
            services.AddScoped<IGestionOTDataSeed, GestionOTDataSeed>();


        }
    }
}
