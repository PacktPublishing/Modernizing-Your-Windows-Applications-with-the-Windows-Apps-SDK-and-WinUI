using System.Data.Entity;
using WinForms_Desktop.Model;

namespace WinForms_Desktop.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmployeeContext() : base(nameof(EmployeeContext))
        {
            Database.SetInitializer<EmployeeContext>(new InitialSeed());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
