using AutoMapper;
using DOU.GestionOT.App.Handlers;
using DOU.GestionOT.App.MVVM.Pages;
using DOU.GestionOT.App.MVVM.Pages.Dashboard;
using DOU.GestionOT.App.MVVM.Pages.Login;
using DOU.GestionOT.App.MVVM.Pages.WorkingDays;
using DOU.GestionOT.App.MVVM.Pages.WorkOrders.FinishedWorkOrders;
using DOU.GestionOT.App.MVVM.Pages.WorkOrders.PendingWorkOrders;
using DOU.GestionOT.App.MVVM.ViewModels;
using DOU.GestionOT.App.MVVM.ViewModels.Dashboard;
using DOU.GestionOT.App.MVVM.ViewModels.Login;
using DOU.GestionOT.App.MVVM.ViewModels.WorkingDays;
using DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.FinishedWorkOrders;
using DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.PendingWorkOrders;
using DOU.GestionOT.App.Services.Ot;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.BL.Services;
using Microsoft.Extensions.Logging;
using DOU.GestionOT.App.Services.Login;

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
                    fonts.AddFont("Ballega.otf", "Ballega");
                    fonts.AddFont("fontello.ttf", "Icons");
                });

            builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
            {
#if ANDROID
                return new Platforms.Android.AndroidHttpMessageHandler();
#else
                return null!;
#endif

            });

            builder.Services.AddHttpClient("custom-httpclient", httpClient =>
            {
                //var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7297" : "https://localhost:7297";
                var baseAddress = "http://192.168.0.60:5057";
                httpClient.BaseAddress = new Uri(baseAddress);
            }).ConfigureHttpMessageHandlerBuilder(configBuilder =>
            {
                var platformMessageHandler = configBuilder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                configBuilder.PrimaryHandler = platformMessageHandler.GetHttpMessageHandler();
            });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Views
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<StudentDashboardPage>();
            builder.Services.AddSingleton<TeacherDashboardPage>();
            builder.Services.AddSingleton<AdminDashboardPage>();
            builder.Services.AddSingleton<WorkingDayPage>();
            builder.Services.AddSingleton<PendingWorkOrdersPage>();
            builder.Services.AddSingleton<PendingWorkOrderDetailPage>();
            builder.Services.AddSingleton<FinishedWorkOrdersPage>();

            // ViewModels
            builder.Services.AddSingleton<AppShellViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<WorkingDayViewModel>();
            builder.Services.AddSingleton<PendingWorkOrdersViewModel>();
            builder.Services.AddSingleton<PendingWorkOrderDetailViewModel>();
            builder.Services.AddSingleton<FinishedWorkOrdersViewModel>();

            // Services
            //builder.Services.AddSingleton<IOtService, OtService>();
            builder.Services.AddSingleton<IOtService, OtAuthService>();
            builder.Services.AddSingleton<IOtBLService, OtBLService>();
            builder.Services.AddSingleton<ILoginService, LoginService>();

            DependencyService.RegisterSingleton(CreateMapper());

            return builder.Build();
        }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMappingProfile>();
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
