using System;

namespace EmployeeManager.Framework.Validation
{
    public interface IValidationResultObserver : IObserver<IValidationResultsPerProperty>
    {
    }
}