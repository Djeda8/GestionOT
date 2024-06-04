using DOU.GestionOT.App.MVVM.ViewModels;
using DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.PendingWorkOrders;

namespace DOU.GestionOT.App.MVVM.Pages.WorkOrders.PendingWorkOrders;

public partial class PendingWorkOrderDetailPage : ContentPage
{
	public PendingWorkOrderDetailPage(PendingWorkOrderDetailViewModel vm)
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