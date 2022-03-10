using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using UWP_Desktop.Contracts;
using UWP_Desktop.Models;

namespace UWP_Desktop.ViewModels
{
    public class DetailsViewModel : ObservableObject
    {
        private readonly IDataService dataService;
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;

        private ICommand cancelCommand;
        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(GoBack));

        private ICommand upsertCommand;
        public ICommand UpsertCommand => upsertCommand ?? (upsertCommand = new AsyncRelayCommand(UpsertEmployee));

        private ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ?? (deleteCommand = new AsyncRelayCommand(DeleteEmployee));
        
        public ObservableEmployee SelectedEmployee { get; set; } = new ObservableEmployee(new Employee());
        public IReadOnlyList<string> GenderList { get; }

        public DetailsViewModel(IDataService dataService, INavigationService navigationService, IDialogService dialogService)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            GenderList = new List<string>(Enum.GetNames(typeof(Gender)));
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
                    "No");

                if (test)
                {
                    await this.dataService.DeleteAsync(SelectedEmployee.Id);
                    GoBack();
                }
            }
        }

        public void GoBack()
        {
            this.navigationService.GoBack();
        }
    }
}
