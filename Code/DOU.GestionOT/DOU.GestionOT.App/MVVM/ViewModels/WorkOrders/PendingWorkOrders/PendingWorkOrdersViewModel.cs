using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DOU.GestionOT.App.MVVM.Models.Ots;
using DOU.GestionOT.App.MVVM.Pages.WorkOrders.PendingWorkOrders;
using System.Collections.ObjectModel;

namespace DOU.GestionOT.App.MVVM.ViewModels.WorkOrders.PendingWorkOrders
{
    public partial class PendingWorkOrdersViewModel : BaseViewModel
    {
        [ObservableProperty]
        private OT selectedOT;

        [ObservableProperty]
        private ObservableCollection<OT> ots;

        public PendingWorkOrdersViewModel()
        {
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

            Ots =
            [
                new()
                {
                    Id = 1,
                    Numero = 32,
                    Serie ="P",
                    Tipo = "PARTE SERVICIOS",
                    Cliente = "CP PLAZA KOLITZA, 1",
                    Direccion = "PLAZA KOLITXA, 1",
                    Fecha = new DateTime(2016, 6, 30, 15, 00, 00),
                    Estado = "INICIADA"
                },
                new()
                {
                    Id = 2,
                    Numero = 34,
                    Serie ="P",
                    Tipo = "PARTE OBRA",
                    Cliente = "CP SENDEJA, 3 - BILBAO",
                    Direccion = "C/ SENDEJA, 3",
                    Fecha = new DateTime(2016, 6, 30, 18, 30, 00),
                    Estado = "PENDIENTE"
                },
                new()
                {
                    Id = 3,
                    Numero = 35,
                    Serie ="P",
                    Tipo = "PARTE CCTV",
                    Cliente = "CP URETAMENDI 49 A 71",
                    Direccion = "C/ URETAMENDI, 49",
                    Fecha = new DateTime(2016, 6, 30, 21, 00, 00),
                    Estado = "PENDIENTE"
                }

            ];
        }
    }
}
