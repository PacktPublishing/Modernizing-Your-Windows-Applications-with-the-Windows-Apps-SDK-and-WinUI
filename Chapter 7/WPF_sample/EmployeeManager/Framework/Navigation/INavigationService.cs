using System.Threading.Tasks;

namespace EmployeeManager.Framework.Navigation
{
    /// <summary>
    /// Provides a centralized access point for the client logic to trigger navigation reuqests.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to the provided view model.
        /// </summary>
        /// <typeparam name="TViewModel">The target view model.</typeparam>
        /// <typeparam name="TParameter">The parameter type passed to the target view model on navigating to it.</typeparam>
        /// <param name="navigationContext">The navigation context, containing the parameter and the intent of the navigation.</param>
        /// <returns>An async navigation result that signals if the navigation was successul or if it was cancelled.</returns>
        Task<NavigationResult> NavigateToViewModel<TViewModel, TParameter>(NavigationContext<TParameter> navigationContext)
            where TViewModel : IViewModel<TParameter>
            where TParameter : INavigationParameter;
    }
}