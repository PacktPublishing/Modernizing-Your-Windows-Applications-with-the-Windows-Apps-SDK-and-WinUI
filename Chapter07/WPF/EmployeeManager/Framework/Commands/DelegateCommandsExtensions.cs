using System;
using System.Threading.Tasks;

namespace EmployeeManager.Framework.Commands
{
    public static class DelegateCommandsExtensions
    {
        public static DelegateCommand CreateAsyncCommand(
            this IBusyIndicator busyIndicator,
            Func<Task> executeMethod,
            Func<bool> canExecuteMethod)
        {
            return DelegateCommand.FromAsyncHandler(executeMethod, canExecuteMethod, busyIndicator);
        }
    }
}