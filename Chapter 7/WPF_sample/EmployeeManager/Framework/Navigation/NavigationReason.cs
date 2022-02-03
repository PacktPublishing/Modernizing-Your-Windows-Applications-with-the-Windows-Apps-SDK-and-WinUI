namespace EmployeeManager.Framework.Navigation
{
    /// <summary>
    /// Communicates the navigation reason.
    /// </summary>
    public enum NavigationReason
    {
        /// <summary>
        /// All automated navigation requests should be default.
        /// </summary>
        Default,
        /// <summary>
        /// Signal an explicit cancel requests to the target view model.
        /// </summary>
        Cancel,
        /// <summary>
        /// Signal an explicit commit request to the target view model.
        /// </summary>
        Commit
    }
}