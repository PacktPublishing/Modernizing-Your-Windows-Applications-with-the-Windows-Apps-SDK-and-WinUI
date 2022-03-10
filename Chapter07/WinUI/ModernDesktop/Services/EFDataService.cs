using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernDesktop.Contracts;
using ModernDesktop.Data;
using ModernDesktop.Model;

namespace ModernDesktop.Services
{
    public class EFDataService : IDataService
    {
        private readonly EmployeeContext context;

        public EFDataService(EmployeeContext dbContext)
        {
            this.context = dbContext;
            this.context.Database.EnsureCreated();
        }

        public IEnumerable<Employee> GetAll()
        {
            return this.context.Employees.AsEnumerable();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await this.context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpsertAsync(Employee employee)
        {
            //obviously this forbids last name changes
            await this.context.Employees.Upsert(employee).On(e => new { e.FirstName, e.LastName }).RunAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            if (employee != null)
            {
                this.context.Employees.Remove(employee);
                await this.context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine($"Failed to delete id {id}");
            }
        }
    }
}
