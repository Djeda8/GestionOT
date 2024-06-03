using DOU.GestionOT.App.MVVM.ViewModels;
using DOU.GestionOT.App.MVVM.ViewModels.Dashboard;

namespace DOU.GestionOT.App.MVVM.Pages.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel vm)
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