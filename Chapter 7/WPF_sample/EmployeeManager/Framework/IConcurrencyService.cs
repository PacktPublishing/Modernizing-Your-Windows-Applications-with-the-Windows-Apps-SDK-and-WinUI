using System.Reactive.Concurrency;

namespace EmployeeManager.Framework
{
    public interface IConcurrencyService
    {
        IScheduler Dispatcher { get; }
        IScheduler TaskPool { get; }
    }
}