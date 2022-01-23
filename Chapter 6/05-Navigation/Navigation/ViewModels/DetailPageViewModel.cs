using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messages.Views;
using Navigation.Contracts;

namespace Navigation.ViewModels
{
    public class DetailPageViewModel: ObservableObject
    {
        private readonly INavigationService _navigationService;

        public RelayCommand GoBackCommand { get; set; }

        public DetailPageViewModel(INavigationService navigationService)
        {
            GoBackCommand = new RelayCommand(GoBack);

            _navigationService = navigationService;
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}
