using System;
using System.Threading.Tasks;

using UWP_Desktop.Services;

using Windows.ApplicationModel.Activation;
using UWP_Desktop.Contracts;

namespace UWP_Desktop.Activation
{
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        private readonly Type navElement;
        private readonly INavigationService navigationService;

        public DefaultActivationHandler(Type navElement, INavigationService navigationService)
        {
            this.navElement = navElement;
            this.navigationService = navigationService;
        }

        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            // When the navigation stack isn't restored, navigate to the first page and configure
            // the new page by passing required information in the navigation parameter
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            this.navigationService.Navigate(this.navElement, arguments);
            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(IActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
            return this.navigationService.Frame.Content == null && this.navElement != null;
        }
    }
}
