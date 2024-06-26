using DOU.GestionOT.App.MVVM.ViewModels;
using DOU.GestionOT.App.MVVM.ViewModels.Login;

namespace DOU.GestionOT.App.MVVM.Pages.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
	    BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        var bindingContext = BindingContext as BaseViewModel;
        bindingContext?.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        var bindingContext = BindingContext as BaseViewModel;
        bindingContext?.OnDisappearing();
    }
}