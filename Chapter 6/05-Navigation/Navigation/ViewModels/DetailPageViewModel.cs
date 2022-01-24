using CommunityToolkit.Mvvm.ComponentModel;
using Navigation.Contracts;
using Navigation.Models;

namespace Navigation.ViewModels
{
    public class DetailPageViewModel: ObservableObject, INavigationAware
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

        public void OnNavigatedTo(object parameter)
        {
            if (parameter != null)
            {
                Person person = parameter as Person;
                Name = person.Name;
                Surname = person.Surname;
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
