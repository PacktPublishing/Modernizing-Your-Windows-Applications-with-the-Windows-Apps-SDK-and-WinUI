using System;
using System.Collections.Generic;
using System.Linq;
using WinForms_Desktop.Model;

namespace WinForms_Desktop.Data
{
    /// <summary>
    ///     A non-thread-safe in memory implementation of an <see cref="IEmployeeRepository" />.
    /// </summary>
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> employees = new List<Employee>();

        public InMemoryEmployeeRepository()
        {
            InitialSeed();
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return this.employees.AsQueryable();
        }

        public Employee GetById(int id)
        {
            return this.employees.FirstOrDefault(x => x.Id == id);
        }

        public void AddOrUpdate(Employee employee)
        {
            var update = this.employees.FirstOrDefault(e => e.Id == employee.Id);
            if (update == null)
            {
                var id = 1;
                if (this.employees.Count > 0)
                {
                    id = this.employees.Count + 1;
                }

                employee.Id = id;
                this.employees.Add(employee);
            }
            else
            {
                update.Address = employee.Address;
                update.City = employee.City;
                update.ZipCode = employee.ZipCode;
                update.DateOfBirth = employee.DateOfBirth;
                update.DateOfHire = employee.DateOfHire;
                update.FirstName = employee.FirstName;
                update.LastName = employee.LastName;
                update.Gender = employee.Gender;
                update.IsMarried = employee.IsMarried;
                update.Role = employee.Role;
                update.Salary = employee.Salary;
            }
        }

        public void Delete(int id)
        {
            var employeeToRemove = this.employees.FirstOrDefault(e => e.Id == id);

            if (employeeToRemove != null)
            {
                this.employees.Remove(employeeToRemove);
            }
        }

        #region InitialData

        private void InitialSeed()
        {
            var faker = new EmployeeFaker("de");
            var data = faker.Generate(25);
            employees.AddRange(data);
        }

        #endregion
    }
}