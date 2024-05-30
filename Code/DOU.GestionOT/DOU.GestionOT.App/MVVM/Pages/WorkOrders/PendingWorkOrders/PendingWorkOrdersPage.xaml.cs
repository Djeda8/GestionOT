using DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.PendingWorkOrders;

namespace DOU.GestionOT.App.MVVM.Pages.WorkOrders.PendingWorkOrders;

public partial class PendingWorkOrdersPage : ContentPage
{
	public PendingWorkOrdersPage(PendingWorkOrdersViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}