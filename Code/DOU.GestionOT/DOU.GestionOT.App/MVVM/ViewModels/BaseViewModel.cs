using CommunityToolkit.Mvvm.ComponentModel;

namespace DOU.GestionOT.App.MVVM.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool _title;

        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearing()
        {
            return Task.CompletedTask;
        }
    }
}
