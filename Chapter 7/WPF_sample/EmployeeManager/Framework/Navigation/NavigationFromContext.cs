using System;

namespace EmployeeManager.Framework.Navigation
{
    /// <summary>
    /// The <see cref="NavigationFromContext"/> communicates the reason and target of a navigation operation. 
    /// The current view model will receive this information and might in turn cancel the navigation request.
    /// </summary>
    public class NavigationFromContext
    {
        public NavigationFromContext(NavigationReason reason, Type targetViewModel)
        {
            Reason = reason;
            TargetViewModel = targetViewModel;
        }

        /// <summary>
        /// The target view model type.
        /// </summary>
        public Type TargetViewModel { get; }

        /// <summary>
        /// The reason for the navigation operation.
        /// </summary>
        public NavigationReason Reason { get; }
    }
}