using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Framework.Validation
{
    public static class ValidationExtensions
    {
        private static ReadOnlyCollection<ValidationResult> Validate(IValidatableModel model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(
                model,
                validationContext,
                validationResults,
                validateAllProperties: true);

            return validationResults.AsReadOnly();
        }

        private static IObservable<IValidationResultsPerProperty> GetPerPropertyErrorMessageObservable(
            this IObservable<IReadOnlyList<ValidationResult>> observable)
        {
            return observable.Select(validationErrors =>
            {
                var flattenedErrors = from validationError in validationErrors
                    from propertyName in validationError.MemberNames
                    select new {PropertyName = propertyName, validationError.ErrorMessage};

                var errorsPerPropertyGrouping = flattenedErrors
                    .GroupBy(_ => _.PropertyName);

                var validationResults = new ValidationResultsPerProperty();
                foreach (var errorsPerPropertyGroup in errorsPerPropertyGrouping)
                {
                    validationResults.Add(
                        errorsPerPropertyGroup.Key,
                        errorsPerPropertyGroup.Select(_ => _.ErrorMessage).ToList().AsReadOnly());
                }

                return validationResults;
            });
        }

        private static IObservable<ModelPropertyChanged> GetPropertyChangedObservableFromValidatableModel(
            this IValidatableModel model)
        {
            return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                handler => model.PropertyChanged += handler,
                handler => model.PropertyChanged -= handler).
                Select(_ =>
                {
                    var viewModel = (IValidatableModel) _.Sender;
                    var propertyName = _.EventArgs.PropertyName;

                    return new ModelPropertyChanged(viewModel, propertyName);
                });
        }

        public static Task<ReadOnlyCollection<ValidationResult>> ValidateAsync(this IValidatableModel model)
        {
            return Task.Factory.StartNew(() => Validate(model));
        }

        public static IObservable<string> GetPropertyChangedObservable(this INotifyPropertyChanged model)
        {
            return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                handler => model.PropertyChanged += handler,
                handler => model.PropertyChanged -= handler).Select(_ => _.EventArgs.PropertyName);
        }

        public static IObservable<IReadOnlyList<ValidationResult>> GetValidationResultsObservable(
            this IValidatableModel model, int throttleMs = 50)
        {
            return model.GetPropertyChangedObservableFromValidatableModel()
                .Where(_ => _.PropertyName != nameof(IBusyIndicator.IsBusy)) // No need to validate on IsBusy changes.
                .Throttle(TimeSpan.FromMilliseconds(throttleMs))
                .Select(modelPropertyChanged => Validate(modelPropertyChanged.Model));
        }

        public static IObservable<IValidatableModel> GetValidationStateChangedObservable(this IValidatableModel model)
        {
            var res = Observable.FromEventPattern<EventHandler<DataErrorsChangedEventArgs>, DataErrorsChangedEventArgs>(
                handler => model.ErrorsChanged += handler,
                handler => model.ErrorsChanged -= handler)
                .Select(_ => (IValidatableModel) _.Sender);

            return res;
        }

        public static IDisposable EnableAutoValidation(
            this IValidatableModel model,
            IScheduler scheduler,
            int throttleMs = 50)
        {
            var subscriptionToken = model
                .GetValidationResultsObservable(throttleMs)
                .GetPerPropertyErrorMessageObservable()
                .ObserveOn(scheduler)
                .Subscribe(model);

            // Force an initial validation.
            model.TriggerAsyncValidation();

            return subscriptionToken;
        }
    }
}