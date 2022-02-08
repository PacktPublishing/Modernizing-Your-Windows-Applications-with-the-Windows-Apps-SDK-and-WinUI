using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernDesktop.Model;

namespace ModernDesktop.Data
{
    public class EmployeeContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Employees;Integrated Security=True");
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        internal DbSet<Employee> Employees { get; set; }
    }
}
