using DOU.GestionOT.App.MVVM.ViewModels.WorkingDays;

namespace DOU.GestionOT.App.MVVM.Pages.WorkingDays;

public partial class WorkingDayPage : ContentPage
{
	public WorkingDayPage(WorkingDayViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}