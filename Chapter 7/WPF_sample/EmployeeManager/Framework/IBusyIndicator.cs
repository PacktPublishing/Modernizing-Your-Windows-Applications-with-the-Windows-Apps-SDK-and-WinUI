using System.ComponentModel;

namespace EmployeeManager.Framework
{
    public interface IBusyIndicator : INotifyPropertyChanged
    {
        bool IsBusy { get; set; }

        bool IsIdle { get; }
    }
}