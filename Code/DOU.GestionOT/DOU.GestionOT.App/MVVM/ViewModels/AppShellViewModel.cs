using CommunityToolkit.Mvvm.Input;
using DOU.GestionOT.App.MVVM.Models;
using DOU.GestionOT.App.MVVM.Pages;
using DOU.GestionOT.App.MVVM.Pages.Dashboard;
using DOU.GestionOT.App.MVVM.Pages.Login;
using DOU.GestionOT.App.MVVM.ViewModels.Startup;
using DOU.GestionOT.App.Utilities;
using Newtonsoft.Json;

namespace DOU.GestionOT.App.MVVM.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {

        public AppShellViewModel()
        {
        }

        [RelayCommand]
        public async Task SignOut()
        {
            SecureStorage.Default.Remove("Authentication");
            GlobalSettings.IsAuthenticated = false;
            //UserName = "Guest";
            await Shell.Current.GoToAsync("..");


            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            Shell.Current.FlyoutIsPresented = false;
        }

        [RelayCommand]
        public async Task Add()
        {
            var flyoutItem = new FlyoutItem()
            {
                Title = "Dashboard Page",
                Route = nameof(AdminDashboardPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Admin Dashboard",
                                    ContentTemplate = new DataTemplate(typeof(AdminDashboardPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Admin Profile",
                                    ContentTemplate = new DataTemplate(typeof(AdminDashboardPage)),
                                },
                   }
            };

            if (!AppShell.Current.Items.Contains(flyoutItem))
            {
                AppShell.Current.Items.Add(flyoutItem);
            }
        }

        public override async Task OnAppearing()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");
            if (!string.IsNullOrWhiteSpace(userDetailsStr))
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
                await AppConstant.AddFlyoutMenusDetails();
            }
        }
    }
}
