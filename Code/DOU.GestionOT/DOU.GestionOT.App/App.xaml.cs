using DOU.GestionOT.App.Handlers;
using DOU.GestionOT.App.MVVM.Models;
using DOU.GestionOT.App.MVVM.Pages;
using DOU.GestionOT.App.MVVM.Pages.Login;
using DOU.GestionOT.App.MVVM.Pages.WorkOrders.PendingWorkOrders;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace DOU.GestionOT.App
{
    public partial class App : Application
    {
        public static UserBasicInfo UserDetails { get; set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            RegisterRoutes();

            MainPage = serviceProvider.GetRequiredService<AppShell>();

            FormHandler.RemoveBorders();
        }

        private void RegisterRoutes()
        {
            // Global Routes
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(PendingWorkOrderDetailPage), typeof(PendingWorkOrderDetailPage));
        }
    }
}
