using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace ModernDesktop.Contracts
{
    public interface INavigationService
    {
        event NavigatedEventHandler Navigated;

        bool CanGoBack { get; }

        Frame Frame { get; }

        bool NavigateTo(Type page, object parameter = null, bool clearNavigation = false);

        bool GoBack();
    }
}
