using System;
using Bogus;
using ModernDesktop.Model;

namespace ModernDesktop.Data
{
    public class EmployeeFaker : Faker<Employee>
    {
        //private int employeeId = 1;
        public EmployeeFaker(string locale = "de") : base(locale)
        {
            //RuleFor(e => e.Id, employeeId++);
            RuleFor(e => e.Gender, f => f.PickRandom<Gender>());
            RuleFor(e => e.FirstName, f => f.Name.FirstName());
            RuleFor(e => e.LastName, f => f.Name.LastName());
            RuleFor(e => e.DateOfBirth, f => f.Date.Past(80, DateTime.UtcNow.AddYears(-20)));
            RuleFor(e => e.Role, f => f.Name.JobTitle());
            RuleFor(e => e.Address, f => f.Address.StreetAddress());
            RuleFor(e => e.ZipCode, f => f.Address.ZipCode());
            RuleFor(e => e.City, f => f.Address.City());
            RuleFor(e => e.Salary, f => f.Finance.Amount(100, 999, 2));
            RuleFor(e => e.IsMarried, f => f.Random.Bool());
            RuleFor(e => e.DateOfHire, f => f.Date.Past(10, DateTime.UtcNow));
        }
    }
}
