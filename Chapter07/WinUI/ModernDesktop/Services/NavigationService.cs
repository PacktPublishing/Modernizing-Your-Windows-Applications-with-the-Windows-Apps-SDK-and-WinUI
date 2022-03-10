using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using ModernDesktop.Contracts;

namespace ModernDesktop.Services
{
    public class NavigationService : INavigationService
    {
        private object _lastParameterUsed;
        private Frame _frame;

        public event NavigatedEventHandler Navigated;

        public Frame Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = App.MainWindow.Content as Frame;
                    RegisterFrameEvents();
                }

                return _frame;
            }
        }

        public bool CanGoBack => Frame.CanGoBack;


        private void RegisterFrameEvents()
        {
            if (Frame != null)
            {
                Frame.Navigated += OnNavigated;
            }
        }

        private void UnregisterFrameEvents()
        {
            if (Frame != null)
            {
                Frame.Navigated -= OnNavigated;
            }
        }

        public bool GoBack()
        {
            if (CanGoBack)
            {
                var vmBeforeNavigation = Frame.GetPageViewModel();
                Frame.GoBack();
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }

                return true;
            }

            return false;
        }

        public bool NavigateTo(Type page, object parameter = null, bool clearNavigation = false)
        {
            if (Frame.Content?.GetType() != page || parameter != null && !parameter.Equals(_lastParameterUsed))
            {
                Frame.Tag = clearNavigation;
                var vmBeforeNavigation = Frame.GetPageViewModel();
                var navigated = Frame.Navigate(page, parameter);
                if (navigated)
                {
                    _lastParameterUsed = parameter;
                    if (vmBeforeNavigation is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatedFrom();
                    }
                }

                return navigated;
            }

            return false;
        }

        public void CleanNavigation()
            => Frame.BackStack.Clear();

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                bool clearNavigation = (bool)frame.Tag;
                if (clearNavigation)
                {
                    frame.BackStack.Clear();
                }

                if (frame.GetPageViewModel() is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.Parameter);
                }

                Navigated?.Invoke(sender, e);
            }
        }
    }

    public static class FrameExtensions
    {
        public static object GetPageViewModel(this Frame frame)
            => frame?.Content?.GetType().GetProperty("ViewModel")?.GetValue(frame.Content, null);
    }
}
