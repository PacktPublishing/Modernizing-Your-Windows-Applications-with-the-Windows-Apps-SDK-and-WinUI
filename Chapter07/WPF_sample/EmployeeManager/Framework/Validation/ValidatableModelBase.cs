using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace EmployeeManager.Framework.Validation
{
    [Serializable]
    public abstract class ValidatableModelBase : NotifyPropertyChangedBase, IValidatableModel
    {
        #region IValidatableModel

        // ReSharper disable once InconsistentNaming
        protected IValidationResultsPerProperty _errorsPerProperty = new ValidationResultsPerProperty();

        [field: NonSerialized]
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (!_errorsPerProperty.ContainsKey(propertyName))
            {
                return Enumerable.Empty<string>();
            }

            return _errorsPerProperty[propertyName];
        }

        public bool HasErrors => _errorsPerProperty.Count > 0;

        public void TriggerAsyncValidation()
        {
            // Force a validation round by triggering a binding update.
            // An even better alternative would be a dedicated event that is joined via Rx.

            // ReSharper disable once ExplicitCallerInfoArgument
            // ReSharper disable once RedundantArgumentDefaultValue
            OnPropertyChanged(null);
        }

        public void OnNext(IValidationResultsPerProperty errorsPerProperty)
        {
            var oldPropertiesInErrorState = _errorsPerProperty.Keys.ToList();
            var newPropertiesInErrorState = errorsPerProperty.Keys.ToList();
            var distinctPropertyNamesToUpdate = newPropertiesInErrorState.Concat(oldPropertiesInErrorState).Distinct();

            _errorsPerProperty = errorsPerProperty;

            var handler = ErrorsChanged;
            if (handler != null)
            {
                foreach (var propertyName in distinctPropertyNamesToUpdate)
                {
                    handler(this, new DataErrorsChangedEventArgs(propertyName));
                }
            }
        }

        public void OnError(Exception error)
        {
            throw new InvalidOperationException("Unexpected call to OnError", error);
        }

        public void OnCompleted()
        {
            RefreshBindings();
        }

        #endregion
    }
}