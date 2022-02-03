using System.ComponentModel;

namespace EmployeeManager.Framework.Validation
{
    public interface IValidatableModel : INotifyPropertyChanged, INotifyDataErrorInfo, IValidationResultObserver
    {
        /// <summary>
        ///     Triggers a validation process. The result is communicated via the <see cref="INotifyDataErrorInfo" /> interface.
        /// </summary>
        void TriggerAsyncValidation();
    }
}