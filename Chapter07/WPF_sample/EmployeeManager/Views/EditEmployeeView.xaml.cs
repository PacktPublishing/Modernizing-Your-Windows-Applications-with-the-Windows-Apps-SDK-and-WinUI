using System.Windows.Threading;
using EmployeeManager.Framework;

namespace EmployeeManager.Views
{
    /// <summary>
    ///     Interaction logic for EditEmployeeView.xaml
    /// </summary>
    public partial class EditEmployeeView : ViewBase
    {
        public EditEmployeeView()
        {
            InitializeComponent();
        }

        EditEmployeeViewModel EditEmployeeViewModel => (EditEmployeeViewModel) ViewModel;

        protected override async void OnViewModelChanged()
        {
            base.OnViewModelChanged();
            await EditEmployeeViewModel.InitializationCompleted;
            await Dispatcher.InvokeAsync(
                () => FirstNameTextBox.Focus(),
                DispatcherPriority.Background);
        }
    }
}