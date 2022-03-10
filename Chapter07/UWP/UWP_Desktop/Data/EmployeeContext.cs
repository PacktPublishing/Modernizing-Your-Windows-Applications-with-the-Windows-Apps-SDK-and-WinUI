using Microsoft.EntityFrameworkCore;
using UWP_Desktop.Models;

namespace UWP_Desktop.Data
{
    public class EmployeeContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Employees;Integrated Security=True");
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var id = 1;
            foreach (var employee in InitialSeed.Seed("en", 25))
            {
                employee.Id = id++;
                modelBuilder.Entity<Employee>().HasData(employee);
            }
        }

        internal DbSet<Employee> Employees { get; set; }
    }
}
