using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messages.Views;
using Navigation.Contracts;

namespace Navigation.ViewModels
{
    public class DetailPageViewModel: ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public RelayCommand GoBackCommand { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public DetailPageViewModel(INavigationService navigationService)
        {
            GoBackCommand = new RelayCommand(GoBack);

            _navigationService = navigationService;
        }

        public void GoBack()
        {   
            _navigationService.GoBack();
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter != null)
            {
                Name = parameter.ToString();
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
