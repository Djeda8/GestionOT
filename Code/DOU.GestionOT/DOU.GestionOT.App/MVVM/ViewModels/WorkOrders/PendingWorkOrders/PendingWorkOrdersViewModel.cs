using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DOU.GestionOT.App.MVVM.Models.Ots;
using DOU.GestionOT.App.MVVM.Pages.WorkOrders.PendingWorkOrders;
using DOU.GestionOT.App.Services.Ot;
using System.Collections.ObjectModel;

namespace DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.PendingWorkOrders
{
    public partial class PendingWorkOrdersViewModel : BaseViewModel
    {
        private readonly IOtService _otService;

        [ObservableProperty]
        private OT selectedOT;

        [ObservableProperty]
        private ObservableCollection<OT> ots;

        public PendingWorkOrdersViewModel(IOtService otService)
        {
            _otService = otService;
            SelectedOT = new OT();
            Ots = [];
        }

        [RelayCommand]
        public async Task SelectionChanged()
        {
            if (SelectedOT is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PendingWorkOrderDetailPage)}?Id={SelectedOT.Id}");
        }

        public override async Task OnAppearing()
        {
            var ots = await _otService.GetOtsAsync();
            Ots =  new(ots);
        }
    }
}
