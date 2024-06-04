using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.PendingWorkOrders
{
    [QueryProperty(nameof(Id), "Id")]
    public partial class PendingWorkOrderDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        private int _id;

        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
