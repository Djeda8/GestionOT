using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DOU.GestionOT.App.MVVM.Models;
using DOU.GestionOT.App.MVVM.Models.Login;
using DOU.GestionOT.App.MVVM.ViewModels.Startup;
using Newtonsoft.Json;
using DOU.GestionOT.App.Services.Login;

namespace DOU.GestionOT.App.MVVM.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private RegisterModel registerModel;
        [ObservableProperty]
        private LoginModel loginModel;

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private bool isAuthenticated;

        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _password;

        private readonly ILoginClientService _clientService;

        public LoginViewModel(ILoginClientService clientService)
        {
            _clientService = clientService;
            RegisterModel = new();
            LoginModel = new();
            IsAuthenticated = false;
        }

        #region Commands
        [RelayCommand]
        public async Task Login()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var userDetails = new UserBasicInfo();
                userDetails.Email = Email;
                userDetails.FullName = "Test User Name";

                // Student Role, Teacher Role, Admin Role,
                if (Email.ToLower().Contains("student"))
                {
                    userDetails.RoleID = (int)RoleDetails.Student;
                    userDetails.RoleText = "Student Role";
                }
                else if (Email.ToLower().Contains("teacher"))
                {
                    userDetails.RoleID = (int)RoleDetails.Teacher;
                    userDetails.RoleText = "Teacher Role";
                }
                else
                {
                    userDetails.RoleID = (int)RoleDetails.Admin;
                    userDetails.RoleText = "Admin Role";
                }


                // calling api
                loginModel.Email = "Admin@gmail.com";
                loginModel.Password = "Admin@123";

                await _clientService.Login(LoginModel);
                await GetUserNameFromSecuredStorage();

                var a = await _clientService.GetWeatherForeCastData();


                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                {
                    Preferences.Remove(nameof(App.UserDetails));
                }

                string userDetailStr = JsonConvert.SerializeObject(userDetails);
                Preferences.Set(nameof(App.UserDetails), userDetailStr);
                App.UserDetails = userDetails;
                await AppConstant.AddFlyoutMenusDetails();
            }
        }

        #endregion
        private async Task GetUserNameFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(UserName) && userName! != "Guest")
            {
                IsAuthenticated = true;
                return;
            }

            string serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                UserName = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.UserName!;
                IsAuthenticated = true;
                return;
            }

            UserName = "Guest";
            IsAuthenticated = false;
        }

        public override async Task OnAppearing()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");
            if (!string.IsNullOrWhiteSpace(userDetailsStr))
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
            }

            await GetUserNameFromSecuredStorage();
        }

        public override async Task OnDisappearing()
        {
        }
    }
}
