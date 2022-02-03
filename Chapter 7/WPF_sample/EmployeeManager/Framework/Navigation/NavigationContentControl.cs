using System.Windows;
using System.Windows.Controls;

namespace EmployeeManager.Framework.Navigation
{
    [TemplatePart(Name = BusyIndicatorTemplatePart)]
    public class NavigationContentControl : ContentControl, INavigationContentControl
    {
        public const string BusyIndicatorTemplatePart = "PART_BusyIndicator";

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            nameof(IsBusy), typeof (bool), typeof (NavigationContentControl),
            new PropertyMetadata(default(bool), PropertyChangedCallback));

        private FrameworkElement _busyIndicator;

        public NavigationContentControl()
        {
            Focusable = false;
        }

        public bool IsBusy
        {
            get { return (bool) GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var self = (NavigationContentControl) dependencyObject;
            var isBusy = (bool) dependencyPropertyChangedEventArgs.NewValue;

            self._busyIndicator.Visibility = isBusy ? Visibility.Visible : Visibility.Hidden;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _busyIndicator = (FrameworkElement) GetTemplateChild(BusyIndicatorTemplatePart);
        }
    }
}