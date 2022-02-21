using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EmployeeManager.Framework
{
    /// <summary>
    ///     Base class for supporting the <see cref="INotifyPropertyChanged" /> interface.
    /// </summary>
    [Serializable]
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);

            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;

            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        ///     Fires a PropertyChangedEvent with <c>null</c> as a parameter.
        ///     This triggers a re-read of all attached bindings.
        /// </summary>
        protected void RefreshBindings()
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(null);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);

            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (propertyName == null)
            {
                // Null is a valid parameter for the PropertyChanged event.
                // --> All Bindings should refresh.
                return;
            }

            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new Exception($"Invalid property name: {propertyName}");
            }
        }
    }
}