using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using EmployeeManager.Framework.Navigation;
using EmployeeManager.Framework.Validation;
using Microsoft.Practices.Unity;

namespace EmployeeManager.Framework
{
    public abstract class ViewModelBase<TContext> :
        ValidatableModelBase, IViewModel<TContext>
        where TContext : INavigationParameter
    {
        private readonly List<Action> _cleanupActions = new List<Action>();
        private readonly TaskCompletionSource<int> _initializationCompletedTaskCompletionSource = new TaskCompletionSource<int>();
        private bool _isBusy;
            
        [Dependency]
        public IConcurrencyService ConcurrencyService { get; set; }

        public Task InitializationCompleted => _initializationCompletedTaskCompletionSource.Task;

        public IView View { get; set; }

        public virtual async Task<NavigationResult> RaiseOnNavigateFrom(NavigationFromContext navigationFromContext)
        {
            using (var busyScope = new BusyIndicatorScope(this))
            {
                return await OnNavigateFrom(navigationFromContext);
            }
        }

        public async Task<NavigationResult> RaiseOnNavigateTo(NavigationContext<TContext> navigationContext)
        {
            using (var busyScope = new BusyIndicatorScope(this))
            {
                try
                {
                    return await OnNavigatedTo(navigationContext);
                }
                finally
                {
                    _initializationCompletedTaskCompletionSource.SetResult(0);
                }
            }
        }

        Task<NavigationResult> INavigationAware.RaiseOnNavigateTo(object navigateToContext)
        {
            return RaiseOnNavigateTo((NavigationContext<TContext>) navigateToContext);
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    // ReSharper disable once ExplicitCallerInfoArgument
                    OnPropertyChanged(nameof(IsIdle));
                }
            }
        }

        public bool IsIdle => !_isBusy;

        public void Dispose()
        {
            OnDispose(true);
        }

        protected virtual Task<NavigationResult> OnNavigatedTo(NavigationContext<TContext> request)
        {
            return Task.FromResult(NavigationResult.Success);
        }

        protected virtual Task<NavigationResult> OnNavigateFrom(NavigationFromContext navigationFromContext)
        {
            return Task.FromResult(NavigationResult.Success);
        }

        protected void AddCleanupAction(Action cleanupAction)
        {
            _cleanupActions.Add(cleanupAction);
        }

        private void OnDispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _cleanupActions.ForEach(cleanUpAction => cleanUpAction());
                _cleanupActions.Clear();
            }
        }

        #region Design Mode Support

        // ReSharper disable once StaticMemberInGenericType
        private static bool? _isInDesignMode;

        /// <summary>
        ///     Gets a value indicating whether the control is in design mode
        ///     (running in Blend or Visual Studio).
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode
                        = (bool) DependencyPropertyDescriptor
                            .FromProperty(prop, typeof (FrameworkElement))
                            .Metadata.DefaultValue;
                }

                return _isInDesignMode.Value;
            }
        }

        #endregion
    }
}