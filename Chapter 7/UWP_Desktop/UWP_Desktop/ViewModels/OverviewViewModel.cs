using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using UWP_Desktop.Contracts;
using UWP_Desktop.Models;
using UWP_Desktop.Services;
using UWP_Desktop.Views;

namespace UWP_Desktop.ViewModels
{
    public class OverviewViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();

        private ICommand _loadedCommand;
        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        private IRelayCommand<Employee> _itemSelectedCommand;
        public IRelayCommand<Employee> ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<Employee>(ItemSelected));
        

        public OverviewViewModel(IDataService dataService, INavigationService navigationService)
        {
            this._dataService = dataService;
            this._navigationService = navigationService;
        }

        private void ItemSelected(Employee employee)
        {
            this._navigationService.Navigate(typeof(DetailsPage), new ObservableEmployee(employee));
        }

        private void OnLoaded()
        {
            LoadData();
        }

        public void LoadData()
        {
            Employees.Clear();
            foreach (var employee in this._dataService.GetAll())
            {
                Employees.Add(employee);
            }
        }
    }
}
