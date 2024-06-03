using DOU.GestionOT.App.MVVM.Models;
using DOU.GestionOT.App.MVVM.Pages;
using DOU.GestionOT.App.MVVM.Pages.Login;

namespace DOU.GestionOT.App
{
    public partial class App : Application
    {
        public static UserBasicInfo UserDetails;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            RegisterRoutes();

            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }

        private void RegisterRoutes()
        {
            // Global Routes
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
