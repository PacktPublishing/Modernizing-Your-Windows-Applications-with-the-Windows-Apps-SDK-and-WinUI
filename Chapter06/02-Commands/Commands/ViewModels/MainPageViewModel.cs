using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Commands.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction,
                () => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname));
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
            Debug.WriteLine($"{Name} {Surname}");
        }
    }
}
