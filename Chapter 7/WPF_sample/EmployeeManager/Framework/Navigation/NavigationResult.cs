namespace EmployeeManager.Framework.Navigation
{
    /// <summary>
    /// The result of a navigation request.
    /// </summary>
    public sealed class NavigationResult
    {
        private NavigationResult(bool cancelNavigation)
        {
            CancelNavigation = cancelNavigation;
        }

        /// <summary>
        /// Create successful navigation result.
        /// </summary>
        public static NavigationResult Success { get; } = new NavigationResult(false);

        /// <summary>
        /// Create a cancelled navigation result.
        /// </summary>
        public static NavigationResult Cancel { get; } = new NavigationResult(true);

        /// <summary>
        /// <c>true</c> if the navigation was successful, <c>false</c> otherwise.
        /// </summary>
        public bool CancelNavigation { get; private set; }
    }
}