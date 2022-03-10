using System;
using System.ComponentModel.DataAnnotations;
using Bogus;
using EmployeeManager.Framework.Validation;
using Name = Bogus.DataSets.Name;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace EmployeeManager.Model
{
    [Serializable]
    public class Employee : ValidatableModelBase
    {
        private string _firstName;
        private DateTime _hiringDate;
        private string _lastName;
        private string _notes;
        private DateTime _birthday;
        private string _role;
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        [Required]
        public DateTime HiringDate
        {
            get => _hiringDate;
            set => SetProperty(ref _hiringDate, value);
        }

        [Required]
        [CustomValidation(typeof(Employee), nameof(ValidateBirthday))]
        public DateTime Birthday
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }

        /// <summary>
        ///     Custom validation via the <see cref="System.ComponentModel" /> validation functionality.
        /// </summary>
        public static ValidationResult ValidateBirthday(DateTime birthday, ValidationContext validationContext)
        {
            var date = DateTime.Now.AddYears(-16);
            if (birthday < date)
            {
                return new ValidationResult($"Employee must be older than 16.", new[] {nameof(HiringDate)});
            }

            return ValidationResult.Success;
        }
    }

    public class EmployeeFaker : Faker<Employee>
    {
        private int employeeId = 0;
        public EmployeeFaker(string locale = "en") : base(locale)
        {
            RuleFor(e => e.Id, f => employeeId++);
            RuleFor(e => e.FirstName, f => f.Name.FirstName());
            RuleFor(e => e.LastName, f => f.Name.LastName());
            RuleFor(e => e.Birthday, f => f.Date.Past(80, DateTime.UtcNow.AddYears(-22)));
            RuleFor(e => e.Role, f => f.Name.JobTitle());
            RuleFor(e => e.HiringDate, f => f.Date.Past(8, DateTime.UtcNow));
        }
    }
}