using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Navigation.Contracts;
using Navigation.Models;
using Navigation.Views;

namespace Navigation.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(IDatabaseService databaseService, INavigationService navigationService)
        {
            SaveCommand = new RelayCommand(SaveAction,
                () => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname));

            _databaseService = databaseService;
            _navigationService = navigationService;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                SetProperty(ref _surname, value);
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }

        private void SaveAction()
        {
            _databaseService.SavePerson(new Person { Name = Name, Surname = Surname });
            _navigationService.NavigateTo(typeof(DetailPage), "Matteo");
        }
    }
}
