using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EmployeeManager.Framework.Validation;
using Microsoft.Practices.Unity;

namespace EmployeeManager.Framework.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly INavigationContentControl _navigationContentControl;
        private readonly IUnityContainer _unityContainer;
        private IDisposable _currentSubscription;

        public NavigationService(IUnityContainer unityContainer, INavigationContentControl navigationContentControl)
        {
            _unityContainer = unityContainer;
            _navigationContentControl = navigationContentControl;
        }

        public Task<NavigationResult> NavigateToViewModel<TViewModel, TContext>(NavigationContext<TContext> navigationContext)
            where TViewModel : IViewModel<TContext>
            where TContext : INavigationParameter
        {
            // Could be cached for a little performance gain.
            var viewModelType = typeof (TViewModel);
            var viewTypeFullName = viewModelType.FullName.Replace(
                viewModelType.Name,
                viewModelType.Name.Replace("ViewModel", "View"));

            var viewType = Type.GetType(viewTypeFullName);
            if (viewType == null)
            {
                throw new Exception(
                    "View-ViewModel naming convention violated." + Environment.NewLine +
                    $"Cannot resolve the View '{viewTypeFullName}' for the " +
                    $"ViewModel '{typeof (TViewModel).FullName}'.");
            }

            return ShowView<TViewModel, TContext>(navigationContext, viewType);
        }

        private async Task<NavigationResult> ShowView<TViewModel, TContext>(
            NavigationContext<TContext> navigationContext, Type viewType)
            where TViewModel : IViewModel<TContext>
            where TContext : INavigationParameter
        {
            try
            {
                var currentVm = GetCurrentViewModel() as INavigationAware;

                if (currentVm != null)
                {
                    var navigationResult = await currentVm
                        .RaiseOnNavigateFrom(new NavigationFromContext(
                            navigationContext.Reason,
                            typeof (TViewModel)));

                    if (navigationResult.CancelNavigation)
                    {
                        return navigationResult;
                    }

                    currentVm.Dispose();
                }

                _currentSubscription?.Dispose();

                var vm = _unityContainer.Resolve<TViewModel>();
                var view = (IView) Activator.CreateInstance(viewType);
                view.ViewModel = vm;
                vm.View = view;

                _currentSubscription = SubscribeToIsBusyChanges(vm);
                _navigationContentControl.Content = view;
                return await vm.RaiseOnNavigateTo(navigationContext);
            }
            finally
            {
                _navigationContentControl.IsBusy = false;
            }
        }

        private object GetCurrentViewModel()
        {
            var view = _navigationContentControl?.Content as IView;
            return view?.ViewModel;
        }

        private IDisposable SubscribeToIsBusyChanges(IBusyIndicator vm)
        {
            return vm.GetPropertyChangedObservable()
                .Where(_ => _ == nameof(IBusyIndicator.IsBusy))
                .Select(_ => vm)
                .Select(_ => _.IsBusy)
                .ObserveOnDispatcher()
                .Subscribe(isBusy => { _navigationContentControl.IsBusy = isBusy; });
        }
    }
}