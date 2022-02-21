using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI.Windows.System;
using CommunityToolkit.Mvvm.ComponentModel;
using ModernDesktop.Model;

namespace ModernDesktop.ViewModels
{
    public class ObservableEmployee : ObservableObject
    {
        private readonly Employee employee;
        public ObservableEmployee(Employee employee) => this.employee = employee;

        public Employee Employee => this.employee;

        public int Id { 
            get => this.employee.Id;
            set => SetProperty(this.employee.Id, value, this.employee, (e, id) => e.Id = id);
        }

        public Gender Gender
        {
            get => this.employee.Gender;
            set => SetProperty(this.employee.Gender, value, this.employee, (e, gender) => e.Gender = gender);
        }

        public string FirstName
        {
            get => this.employee.FirstName;
            set => SetProperty(this.employee.FirstName, value, this.employee, (e, fn) => e.FirstName = fn);
        }

        public string LastName
        {
            get => this.employee.LastName;
            set => SetProperty(this.employee.LastName, value, this.employee, (e, ln) => e.LastName = ln);
        }
        public DateTimeOffset DateOfBirth
        {
            get => new(this.employee.DateOfBirth,TimeSpan.Zero);
            set => SetProperty(this.employee.DateOfBirth, value, this.employee, (e, db) => e.DateOfBirth = db.UtcDateTime);
        }
        public string Role
        {
            get => this.employee.Role;
            set => SetProperty(this.employee.Role, value, this.employee, (e, role) => e.Role = role);
        }
        public string Address
        {
            get => this.employee.Address;
            set => SetProperty(this.employee.Address, value, this.employee, (e, ad) => e.Address = ad);
        }
        public string ZipCode
        {
            get => this.employee.ZipCode;
            set => SetProperty(this.employee.ZipCode, value, this.employee, (e, zip) => e.ZipCode = zip);
        }
        public string City
        {
            get => this.employee.City;
            set => SetProperty(this.employee.City, value, this.employee, (e, city) => e.City = city);
        }
        public bool IsMarried
        {
            get => this.employee.IsMarried;
            set => SetProperty(this.employee.IsMarried, value, this.employee, (e, m) => e.IsMarried = m);
        }
        public decimal Salary
        {
            get => this.employee.Salary;
            set => SetProperty(this.employee.Salary, value, this.employee, (e, s) => e.Salary = s);
        }
        public DateTimeOffset DateOfHire
        {
            get => new(this.employee.DateOfHire, TimeSpan.Zero);
            set => SetProperty(this.employee.DateOfHire, value, this.employee, (e, dh) => e.DateOfHire = dh.UtcDateTime);
        }
    }
}
