using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Navigation.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            App.ShellFrame = ShellFrame;
            ShellFrame.Content = new HomePage();
        }


        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag)
            {
                case "Home":
                    ShellFrame.Navigate(typeof(HomePage));
                    break;
                case "Favorite":
                    ShellFrame.Navigate(typeof(FavoritesPage));
                    break;
                case "List":
                    ShellFrame.Navigate(typeof(ListPage));
                    break;
                case "NavigationMode":
                    nvgView.PaneDisplayMode = nvgView.PaneDisplayMode == NavigationViewPaneDisplayMode.Auto
                        ? NavigationViewPaneDisplayMode.Top
                        : NavigationViewPaneDisplayMode.Auto;
                    break;
                case "TopMenu":
                    ShellFrame.Navigate(typeof(TopMenuPage));
                    break;
            }

            if (args.IsSettingsInvoked)
            {
                ShellFrame.Navigate(typeof(SettingsPage));
            }
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ShellFrame.CanGoBack)
            {
                ShellFrame.GoBack();
            }
        }
    }
}
