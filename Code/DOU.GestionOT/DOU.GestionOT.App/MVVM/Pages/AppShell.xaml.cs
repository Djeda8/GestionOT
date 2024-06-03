using DOU.GestionOT.App.MVVM.ViewModels;
using System.Diagnostics;

namespace DOU.GestionOT.App.MVVM.Pages;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);

        Debug.WriteLine($"--- A navigation was performed: {args.Source}, from {args.Previous?.Location} to {args.Current?.Location}");
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
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