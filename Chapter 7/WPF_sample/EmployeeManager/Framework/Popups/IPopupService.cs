using System.Threading.Tasks;

namespace EmployeeManager.Framework.Popups
{
    /// <summary>
    /// Provider service for popups.
    /// </summary>
    public interface IPopupService
    {
        /// <summary>
        /// Shows a confirmation dialog.
        /// </summary>
        /// <param name="content">The presented message.</param>
        /// <param name="title">The title.</param>
        /// <returns>True; if confirmed. False otherwise.</returns>
        Task<bool> ShowConfirmation(string content, string title);
    }
}