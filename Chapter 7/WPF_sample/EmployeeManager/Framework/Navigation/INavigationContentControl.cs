namespace EmployeeManager.Framework.Navigation
{
    /// <summary>
    /// Provides an interface to manage the active content on a content control.
    /// </summary>
    public interface INavigationContentControl
    {
        /// <summary>
        /// If set to <c>true</c>, a visible feedback is shown to the user.
        /// </summary>
        bool IsBusy { get; set; }

        /// <summary>
        /// Sets the active content.
        /// </summary>
        object Content { get; set; }
    }
}