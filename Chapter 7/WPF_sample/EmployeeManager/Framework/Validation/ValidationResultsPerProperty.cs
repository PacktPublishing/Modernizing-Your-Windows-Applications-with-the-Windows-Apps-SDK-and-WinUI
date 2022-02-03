using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace EmployeeManager.Framework.Validation
{
    [Serializable]
    public class ValidationResultsPerProperty : Dictionary<string, ReadOnlyCollection<string>>,
        IValidationResultsPerProperty
    {
        public ValidationResultsPerProperty()
        {
        }

        public ValidationResultsPerProperty(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}