using System;
using System.Threading.Tasks;
using EmployeeManager.Framework.Navigation;

namespace EmployeeManager.Framework
{
    public interface INavigationAware : IDisposable
    {
        Task<NavigationResult> RaiseOnNavigateFrom(NavigationFromContext navigationRequest);
        Task<NavigationResult> RaiseOnNavigateTo(object navigateToContext);
    }

    public interface IViewModel<TContext> : INavigationAware, IBusyIndicator
        where TContext : INavigationParameter
    {
        /// <summary>
        ///     Technical reference to view. Not intended for direct usage.
        /// </summary>
        IView View { get; set; }

        Task<NavigationResult> RaiseOnNavigateTo(NavigationContext<TContext> navigationContext);
    }
}