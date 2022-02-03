namespace EmployeeManager.Framework.Navigation
{
    /// <summary>
    /// The default (empty) implementation for the <see cref="INavigationParameter"/>.
    /// </summary>
    public sealed class NavigationParameter : INavigationParameter
    {
        public static readonly NavigationParameter Default = new NavigationParameter();

        private NavigationParameter()
        {
        }
    }
}