using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManager.Framework;
using EmployeeManager.Framework.Validation;

namespace EmployeeManager.Model
{
    /// <summary>
    ///     A non-thread-safe in memory implementation of an <see cref="IEmployeeRepository" />.
    /// </summary>
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new List<Employee>();
        private readonly Random _random = new Random();
        private readonly bool _withDelay = true;

        public InMemoryEmployeeRepository()
        {
            SeedWithData();
        }

        public IQueryable<Employee> Query()
        {
            return _employees.Select(x =>
            {
                // Simulate an I/O overhead for executing the query.
                if (_withDelay)
                {
                    Thread.Sleep(_random.Next(20, 80));
                }

                return x.Clone();
            }).AsQueryable();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            await GetDelay();
            return _employees.Where(x => x.Id == id).Select(x => x.Clone()).FirstOrDefault();
        }

        public async Task UpdateAsync(Employee employee)
        {
            await GetDelay();

            var validationResults = await employee.ValidateAsync();
            if (validationResults.Count != 0)
            {
                throw new ArgumentException("Invalid employee record. Cannot update.", nameof(employee));
            }

            var employeeToUpdate = _employees.FirstOrDefault(r => r.Id == employee.Id);
            if (employeeToUpdate == null)
            {
                throw new ArgumentException("Unknown employee. Cannot update.", nameof(employee));
            }

            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.HiringDate = employee.HiringDate;
            employeeToUpdate.Birthday = employee.Birthday;
            employeeToUpdate.Role = employee.Role;
        }

        public async Task DeleteAsync(Employee employee)
        {
            await GetDelay();

            var employeeToRemove = _employees.FirstOrDefault(r => r.Id == employee.Id);

            if (employeeToRemove != null)
            {
                _employees.Remove(employeeToRemove);
            }
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await GetDelay();

            var validationResults = await employee.ValidateAsync();
            if (validationResults.Count != 0)
            {
                throw new ArgumentException("Invalid employee record. Cannot update.", nameof(employee));
            }

            int newId;

            if (_employees.Count > 0)
            {
                newId = _employees.Max(x => x.Id) + 1;
            }
            else
            {
                newId = 1;
            }

            employee.Id = newId;
            _employees.Add(employee.Clone());

            return employee;
        }

        public Task GetDelay()
        {
            return _withDelay ? Task.Delay(_random.Next(500, 2000)) : Task.CompletedTask;
        }

        #region InitialData

        private void SeedWithData()
        {
            var faker = new EmployeeFaker("de");
            var data = faker.Generate(25);
            _employees.AddRange(data);
        }

        #endregion
    }
}