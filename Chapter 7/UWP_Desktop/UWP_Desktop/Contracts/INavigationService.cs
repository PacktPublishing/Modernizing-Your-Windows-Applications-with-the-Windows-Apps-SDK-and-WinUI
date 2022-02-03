using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UWP_Desktop.Contracts
{
    public interface INavigationService
    {
        event NavigatedEventHandler Navigated;
        event NavigationFailedEventHandler NavigationFailed;
        Frame Frame { get; set; }
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        bool GoBack();
        void GoForward();
        bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null);

        bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null)
            where T : Page;
    }
}
