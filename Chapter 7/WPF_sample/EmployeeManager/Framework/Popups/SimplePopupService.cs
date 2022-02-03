using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager.Framework.Popups
{
    /// <summary>
    ///     The <see cref="SimplePopupService" /> just wraps the MessageBox class.
    ///     Not something you are likely doing in a real world application.
    /// </summary>
    public class SimplePopupService : IPopupService
    {
        public Task<bool> ShowConfirmation(string content, string title)
        {
            var result = MessageBox.Show(content, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return Task.FromResult(result == MessageBoxResult.Yes);
        }
    }
}