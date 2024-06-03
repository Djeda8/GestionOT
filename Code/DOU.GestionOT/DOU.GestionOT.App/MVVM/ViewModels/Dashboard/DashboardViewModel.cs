using DOU.GestionOT.App.Control;
using DOU.GestionOT.App.MVVM.Models;
using DOU.GestionOT.App.MVVM.Pages;
using DOU.GestionOT.App.MVVM.Pages.Login;
using DOU.GestionOT.App.MVVM.ViewModels.Startup;
using Newtonsoft.Json;

namespace DOU.GestionOT.App.MVVM.ViewModels.Dashboard
{
    public partial class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel()
        {

        }

        private async Task CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
                }
                // navigate to Login Page
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
                // await AppConstant.AddFlyoutMenusDetails();
                // navigate to Dashboard
            }
        }

        public override async Task OnAppearing()
        {
            var userDetailsStr = App.UserDetails;
            if (userDetailsStr is null)
                await CheckUserLoginDetails();

        }

        public override async Task OnDisappearing()
        {
        }
    }
}
