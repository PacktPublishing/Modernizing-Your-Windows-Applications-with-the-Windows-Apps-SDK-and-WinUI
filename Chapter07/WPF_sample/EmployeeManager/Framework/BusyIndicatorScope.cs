using System;

namespace EmployeeManager.Framework
{
    public class BusyIndicatorScope : IDisposable
    {
        private IBusyIndicator _busyIndicator;
        private bool _isDisposed;

        public BusyIndicatorScope(IBusyIndicator busyIndicator)
        {
            if (busyIndicator == null)
            {
                throw new ArgumentNullException(nameof(busyIndicator));
            }

            _busyIndicator = busyIndicator;
            _busyIndicator.IsBusy = true;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _busyIndicator.IsBusy = false;
            _busyIndicator = null;
            _isDisposed = true;
        }
    }
}