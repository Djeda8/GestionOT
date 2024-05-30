using Microsoft.Extensions.Logging;
using DOU.GestionOT.App.MVVM.Pages.Login;
using DOU.GestionOT.App.MVVM.Pages.WorkingDays;
using DOU.GestionOT.App.MVVM.Pages.WorkOrders.FinishedWorkOrders;
using DOU.GestionOT.App.MVVM.Pages.WorkOrders.PendingWorkOrders;
using DOU.GestionOT.App.MVVM.ViewModels.Login;
using DOU.GestionOT.App.MVVM.ViewModels.WorkingDays;
using DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.FinishedWorkOrders;
using DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.PendingWorkOrders;

namespace DOU.GestionOT.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fontello.ttf", "Icons");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<WorkingDayPage>();
            builder.Services.AddTransient<PendingWorkOrdersPage>();
            builder.Services.AddTransient<FinishedWorkOrdersPage>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<WorkingDayViewModel>();
            builder.Services.AddTransient<PendingWorkOrdersViewModel>();
            builder.Services.AddTransient<FinishedWorkOrdersViewModel>();

            return builder.Build();
        }
    }
}
