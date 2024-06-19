using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DOU.GestionOT.App.MVVM.Models;
using DOU.GestionOT.App.MVVM.ViewModels.Startup;
using Newtonsoft.Json;

namespace DOU.GestionOT.App.MVVM.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _password;

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

        public override async Task OnAppearing()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");
            if (!string.IsNullOrWhiteSpace(userDetailsStr))
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
            }
        }

        public override async Task OnDisappearing()
        {
        }
    }
}
