using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernDesktop.Contracts;
using ModernDesktop.Model;
using ModernDesktop.Views;

namespace ModernDesktop.ViewModels
{
    public class DetailsViewModel : ObservableObject, INavigationAware
    {
        private readonly IDataService dataService;
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;

        public IAsyncRelayCommand DeleteCommand { get; private set; }
        public IAsyncRelayCommand UpsertCommand { get; private set; }
        public IRelayCommand CancelCommand { get; private set; }
        public bool IsExisting { get; set; }
        public ObservableEmployee SelectedEmployee { get; set; } = new(new Employee());
        public IReadOnlyList<string> GenderList { get; } = new List<string>(Enum.GetNames<Gender>());

        public DetailsViewModel(IDataService dataService, INavigationService navigationService, IDialogService dialogService)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            InitializeCommands();
            //LoadEmployee();
        }

        private async Task<Employee> LoadEmployee()
        {
            var id = 1;
            return await this.dataService.GetByIdAsync(id);
        }

        private void InitializeCommands()
        {
            DeleteCommand = new AsyncRelayCommand(DeleteEmployee);
            UpsertCommand = new AsyncRelayCommand(UpsertEmployee);
            CancelCommand = new RelayCommand(GoBack);
        }

        private async Task UpsertEmployee()
        {
            if (SelectedEmployee != null) await this.dataService.UpsertAsync(SelectedEmployee.Employee);
            GoBack();
        }

        private async Task DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                var test = await this.dialogService.ShowAsync("Delete", "Do you really want to delete the employee?",
                    "Yes",
                    "No", App.MainWindow.Content.XamlRoot);

                if (test)
                {
                    await this.dataService.DeleteAsync(SelectedEmployee.Id);
                    GoBack();
                }
            }
        }

        public void GoBack()
        {
            this.navigationService.NavigateTo(typeof(MainPage), null, true);
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is ObservableEmployee employee)
            {
                SelectedEmployee = employee;
                IsExisting = true;
            }
            else
            {
                SelectedEmployee = new ObservableEmployee(new Employee());
                IsExisting = false;
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
