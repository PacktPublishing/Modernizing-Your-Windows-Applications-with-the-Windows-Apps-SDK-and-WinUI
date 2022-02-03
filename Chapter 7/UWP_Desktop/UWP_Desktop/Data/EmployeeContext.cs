using Microsoft.EntityFrameworkCore;
using UWP_Desktop.Models;

namespace UWP_Desktop.Data
{
    public class EmployeeContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Employees;Integrated Security=True");
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) {}

        internal DbSet<Employee> Employees { get; set; }
    }
}
