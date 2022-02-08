namespace EmployeeManager.Framework.Navigation
{
    public class NavigationContext<TParameter> where TParameter : INavigationParameter
    {
        protected NavigationContext()
        {
        }

        /// <summary>
        /// Create a <see cref="NavigationReason.Default"/> <see cref="NavigationContext"/>.
        /// </summary>
        /// <param name="parameter">Parameter passed to the view model.</param>
        public NavigationContext(TParameter parameter) : this(parameter, NavigationReason.Default)
        {
        }

        /// <summary>
        /// Create a new <see cref="NavigationContext"/>.
        /// </summary>
        /// <param name="parameter">Parameter passed to the view model.</param>
        /// <param name="navigationReason">The reason for the navigation process.</param>
        public NavigationContext(TParameter parameter, NavigationReason navigationReason)
        {
            Parameter = parameter;
            Reason = navigationReason;
        }

        /// <summary>
        /// Parameter passed to the view model.
        /// </summary>
        public TParameter Parameter { get; }

        /// <summary>
        /// The navigation reason passed to the view model.
        /// </summary>
        public NavigationReason Reason { get; }
    }

    public sealed class NavigationContext : NavigationContext<NavigationParameter>
    {
        public NavigationContext() : base(NavigationParameter.Default)
        {
        }

        public NavigationContext(NavigationReason navigationReason)
            : base(NavigationParameter.Default, navigationReason)
        {
        }

        public static NavigationContext Default { get; } = new NavigationContext();
    }
}