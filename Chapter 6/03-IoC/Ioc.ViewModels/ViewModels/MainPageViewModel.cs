using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IoC.Models;
using IoC.Services;
using System.Diagnostics;

namespace IoC.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private IDatabaseService _databaseService;

        public MainViewModel(IDatabaseService databaseService)
        {
            SaveCommand = new RelayCommand(SaveAction,
                () => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname));

            _databaseService = databaseService;
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
        }
    }
}
