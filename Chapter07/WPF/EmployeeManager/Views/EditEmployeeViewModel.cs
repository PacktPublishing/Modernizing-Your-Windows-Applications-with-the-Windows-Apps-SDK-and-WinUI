using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EmployeeManager.Framework;
using EmployeeManager.Framework.Commands;
using EmployeeManager.Framework.Navigation;
using EmployeeManager.Framework.Popups;
using EmployeeManager.Framework.Validation;
using EmployeeManager.Model;
using EmployeeManager.Properties;

namespace EmployeeManager.Views
{
    public class EditEmployeeViewModel : ViewModelBase<EditEmployeeParameter>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;
        private EditEmployeeParameter _currentParameter;
        private string _editDialogTitle;
        private Employee _selectedEmployee;
        private bool _isPristine;
        private bool _isAutovalidating;

        public EditEmployeeViewModel(IEmployeeRepository employeeRepository, INavigationService navigationService,
            IPopupService popupService)
        {
            _employeeRepository = employeeRepository;
            _navigationService = navigationService;
            _popupService = popupService;

            Delete = this.CreateAsyncCommand(OnDeleteExecute, OnDeleteCanExecute);
            //SaveEmployee = this.CreateAsyncCommand(OnSaveEmployeeExecute, OnSaveEmployeeCanExecute);

            Ok = this.CreateAsyncCommand(OnOk, OnOkCanExecute);
            Cancel = this.CreateAsyncCommand(() =>
                _navigationService.NavigateToViewModel<OverviewViewModel, NavigationParameter>(NavigationContext.Default),
                () => true);
        }

        public DelegateCommand Ok { get; }

        public DelegateCommand Cancel { get; private set; }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { SetProperty(ref _selectedEmployee, value); }
        }

        public string EditDialogTitle
        {
            get { return _editDialogTitle; }
            set { SetProperty(ref _editDialogTitle, value); }
        }

        private async Task OnOk()
        {
            var hasValidationErrors = (await SelectedEmployee.ValidateAsync()).Count != 0;
            if (hasValidationErrors)
            {
                if (!_isAutovalidating)
                {
                    _isAutovalidating = true;
                    var autoValidationToken = SelectedEmployee.EnableAutoValidation(ConcurrencyService.Dispatcher);
                    AddCleanupAction(() => autoValidationToken.Dispose());
                }

                return;
            }

            switch (_currentParameter.EditAction)
            {
                case EditAction.Edit:
                    await _employeeRepository.UpdateAsync(SelectedEmployee);
                    break;
                case EditAction.Add:
                    await _employeeRepository.AddAsync(SelectedEmployee);
                    break;
            }

            await _navigationService.NavigateToViewModel<OverviewViewModel, NavigationParameter>(
                new NavigationContext<NavigationParameter>(NavigationParameter.Default, NavigationReason.Commit));
        }

        private bool OnOkCanExecute()
        {
            return SelectedEmployee != null && !SelectedEmployee.HasErrors;
        }

        protected override async Task<NavigationResult> OnNavigateFrom(NavigationFromContext navigationFromContext)
        {
            if (!_isPristine && navigationFromContext.Reason != NavigationReason.Commit)
            {
                var shouldLeave = await _popupService.ShowConfirmation("Unsaved changes. Are you sure?", "Confirmation");
                if (!shouldLeave)
                {
                    return NavigationResult.Cancel;
                }
            }

            return await base.OnNavigateFrom(navigationFromContext);
        }

        protected override async Task<NavigationResult> OnNavigatedTo(NavigationContext<EditEmployeeParameter> request)
        {
            _currentParameter = request.Parameter;
            _isPristine = true;
            switch (_currentParameter.EditAction)
            {
                case EditAction.Edit:
                    SelectedEmployee = await _employeeRepository.GetByIdAsync(_currentParameter.EmployeeId);
                    EditDialogTitle = Resources.EditEmployeeViewModel_Edit_Title;
                    break;
                case EditAction.Add:
                    EditDialogTitle = Resources.EditEmployeeViewModel_Add_Title;
                    SelectedEmployee = new Employee {HiringDate = DateTime.UtcNow};
                    break;
            }
            
            var employeeChangedToken = SelectedEmployee
                .GetPropertyChangedObservable()
                .ObserveOn(ConcurrencyService.Dispatcher)
                .Subscribe(_ =>
                {
                    Delete.RaiseCanExecuteChanged();
                    //SaveEmployee.RaiseCanExecuteChanged();
                    _isPristine = false;
                });
            AddCleanupAction(() => employeeChangedToken.Dispose());

            // Observe validation and input changes to raise command updates.
            var stateChangesToken = SelectedEmployee
                .GetValidationStateChangedObservable()
                .Select(_ => int.MinValue) // ignored
                .Merge(SelectedEmployee.GetPropertyChangedObservable().Select(_ => int.MinValue)) // ignored
                .ObserveOn(ConcurrencyService.Dispatcher)
                .Subscribe(_ => Ok.RaiseCanExecuteChanged());
            AddCleanupAction(() => stateChangesToken.Dispose());

            return await base.OnNavigatedTo(request);
        }


        #region DeleteCommand

        public DelegateCommand Delete { get; }

        public async Task OnDeleteExecute()
        {
            var selectedEmployee = SelectedEmployee;
            await _employeeRepository.DeleteAsync(selectedEmployee);
            //var oldIndex = Employees.IndexOf(selectedEmployee);
            //Employees.Remove(selectedEmployee);
            //var newIndex = oldIndex >= Employees.Count ? Employees.Count - 1 : oldIndex;
            //SelectedEmployee = newIndex == -1 ? null : Employees[newIndex];
        }

        public bool OnDeleteCanExecute()
        {
            return SelectedEmployee != null;
        }

        #endregion


        //#region EditCommand

        //public DelegateCommand SaveEmployee { get; }

        //public async Task OnSaveEmployeeExecute()
        //{
        //    var employeeId = SelectedEmployee.Id;
        //    await
        //        _navigationService.NavigateToViewModel<EditEmployeeViewModel, EditEmployeeParameter>(
        //            new NavigationContext<EditEmployeeParameter>(
        //                EditEmployeeParameter.CreateEditEmployeeParameter(employeeId)));
        //}

        //public bool OnSaveEmployeeCanExecute()
        //{
        //    return SelectedEmployee != null;
        //}

        //#endregion
    }
}