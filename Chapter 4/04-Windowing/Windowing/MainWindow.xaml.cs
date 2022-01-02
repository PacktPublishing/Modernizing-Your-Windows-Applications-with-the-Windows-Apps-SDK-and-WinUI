using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

namespace Windowing
{

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private void OnMoveWindow(object sender, RoutedEventArgs e)
        {
            var appWindow = GetAppWindowForCurrentWindow();
            appWindow.Move(new Windows.Graphics.PointInt32(300, 300));
            appWindow.Resize(new Windows.Graphics.SizeInt32(800, 600));

            //appWindow.MoveAndResize(new Windows.Graphics.RectInt32(300, 300, 800, 600));
        }

        private void OnSetTitle(object sender, RoutedEventArgs e)
        {
            var appWindow = GetAppWindowForCurrentWindow();
            appWindow.Title = "This is a custom title";
        }

        private void OnCustomizeTitleBar(object sender, RoutedEventArgs e)
        {
            var appWindow = GetAppWindowForCurrentWindow();
            appWindow.Title = "This is a custom title";
            appWindow.TitleBar.ForegroundColor = Colors.White;
            appWindow.TitleBar.BackgroundColor = Colors.DarkOrange;

            //Buttons
            appWindow.TitleBar.ButtonBackgroundColor = Colors.DarkOrange;
            appWindow.TitleBar.ButtonForegroundColor = Colors.White;

        }

        private void OnSetCustomTitleBar(object sender, RoutedEventArgs e)
        {
            var appWindow = GetAppWindowForCurrentWindow();
            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;

            // Show the custom titlebar
            MyTitleBar.Visibility = Visibility.Visible;

            //Infer titlebar height
            int titleBarHeight = appWindow.TitleBar.Height;
            this.MyTitleBar.Height = titleBarHeight;

        }

        private void OnSetFullScreenPresenter(object sender, RoutedEventArgs e)
        {
            var appWindow = GetAppWindowForCurrentWindow();
            if (appWindow.Presenter.Kind is not AppWindowPresenterKind.FullScreen)
            {
                appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            }
            else
            {
                appWindow.SetPresenter(AppWindowPresenterKind.Default);
            }

        }

        private void OnSetCompactOverlayPresenter(object sender, RoutedEventArgs e)
        {
            var appWindow = GetAppWindowForCurrentWindow();
            if (appWindow.Presenter.Kind is not AppWindowPresenterKind.FullScreen)
            {
                appWindow.SetPresenter(AppWindowPresenterKind.CompactOverlay);
            }
            else
            {
                appWindow.SetPresenter(AppWindowPresenterKind.Default);
            }

        }
    }
}
