using DOU.GestionOT.App.MVVM.ViewModels.Login;

namespace DOU.GestionOT.App.MVVM.Pages.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
	    BindingContext = vm;
	}
}