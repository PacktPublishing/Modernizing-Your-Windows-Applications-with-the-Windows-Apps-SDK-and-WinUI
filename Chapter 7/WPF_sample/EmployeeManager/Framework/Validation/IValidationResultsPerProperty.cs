using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManager.Framework.Validation
{
    public interface IValidationResultsPerProperty : IReadOnlyDictionary<string, ReadOnlyCollection<string>>
    {
    }
}