using DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.FinishedWorkOrders;

namespace DOU.GestionOT.App.MVVM.Pages.WorkOrders.FinishedWorkOrders;

public partial class FinishedWorkOrdersPage : ContentPage
{
	public FinishedWorkOrdersPage(FinishedWorkOrdersViewModel vm)
	{
		InitializeComponent();
		BindingContext  = vm;
	}
}