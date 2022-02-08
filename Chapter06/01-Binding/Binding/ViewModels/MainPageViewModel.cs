using CommunityToolkit.Mvvm.ComponentModel;

namespace Binding.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { SetProperty(ref _surname, value); }
        }
    }

}
