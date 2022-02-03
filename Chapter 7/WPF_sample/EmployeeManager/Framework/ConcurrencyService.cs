using System.Reactive.Concurrency;

namespace EmployeeManager.Framework
{
    public sealed class ConcurrencyService : IConcurrencyService
    {
        public IScheduler Dispatcher => DispatcherScheduler.Current;
        public IScheduler TaskPool => ThreadPoolScheduler.Instance;
    }
}