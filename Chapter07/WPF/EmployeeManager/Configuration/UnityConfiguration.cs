using EmployeeManager.Framework;
using EmployeeManager.Framework.Navigation;
using EmployeeManager.Framework.Popups;
using EmployeeManager.Model;
using Microsoft.Practices.Unity;

namespace EmployeeManager.Configuration
{
    public static class UnityConfiguration
    {
        public static IUnityContainer CreateContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterType<IEmployeeRepository, InMemoryEmployeeRepository>(
                new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<IConcurrencyService, ConcurrencyService>(
                new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<INavigationService, NavigationService>(
                new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<IPopupService, SimplePopupService>(
                new ContainerControlledLifetimeManager());

            return unityContainer;
        }
    }
}