using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EmployeeManager.Framework;
using EmployeeManager.Framework.Commands;
using EmployeeManager.Framework.Navigation;
using EmployeeManager.Framework.Validation;
using EmployeeManager.Model;

namespace EmployeeManager.Views
{
    public class OverviewViewModel : ViewModelBase<NavigationParameter>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INavigationService _navigationService;
        private Employee _selectedEmployee;

        public OverviewViewModel(
            IEmployeeRepository employeeRepository,
            INavigationService navigationService)
        {
            _employeeRepository = employeeRepository;
            _navigationService = navigationService;

            Employees = new ObservableCollection<Employee>();

            SelectedItem = this.CreateAsyncCommand(OnSelectedItemExecute, OnSelectedItemCanExecute);
            AddEmployee = this.CreateAsyncCommand(OnAddEmployeeExecute, OnAddEmployeeCanExecute);
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { SetProperty(ref _selectedEmployee, value); }
        }

        public ObservableCollection<Employee> Employees { get; }

        protected override async Task<NavigationResult> OnNavigatedTo(NavigationContext<NavigationParameter> request)
        {
            var employees = await _employeeRepository.Query().ToListAsync();
            Employees.Replace(employees);

            var myPropertiesChangeToken =
                this.GetPropertyChangedObservable()
                    .ObserveOn(ConcurrencyService.Dispatcher)
                    .Subscribe(_ =>
                    {
                        SelectedItem.RaiseCanExecuteChanged();
                        AddEmployee.RaiseCanExecuteChanged();
                    });

            AddCleanupAction(() => myPropertiesChangeToken.Dispose());

            return await base.OnNavigatedTo(request);
        }

        public DelegateCommand SelectedItem { get; }

        public async Task OnSelectedItemExecute()
        {
            var employeeId = SelectedEmployee.Id;
            await
                _navigationService.NavigateToViewModel<EditEmployeeViewModel, EditEmployeeParameter>(
                    new NavigationContext<EditEmployeeParameter>(
                        EditEmployeeParameter.CreateEditEmployeeParameter(employeeId)));
        }

        public bool OnSelectedItemCanExecute()
        {
            return SelectedEmployee != null;
        }

        #region AddCommand

        public DelegateCommand AddEmployee { get; }

        public async Task OnAddEmployeeExecute()
        {
            await
                _navigationService.NavigateToViewModel<EditEmployeeViewModel, EditEmployeeParameter>(
                    new NavigationContext<EditEmployeeParameter>(EditEmployeeParameter.CreateAddEmployeeParameter()));
        }

        public bool OnAddEmployeeCanExecute()
        {
            return true;
        }

        #endregion
    }
}