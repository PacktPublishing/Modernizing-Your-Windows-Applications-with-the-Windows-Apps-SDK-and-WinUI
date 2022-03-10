using System.Data.Entity;
using WinForms_Desktop.Model;

namespace WinForms_Desktop.Data
{
    public class InitialSeed : CreateDatabaseIfNotExists<EmployeeContext>
    {
        protected override void Seed(EmployeeContext context)
        {
            var faker = new EmployeeFaker("de");
            var data = faker.Generate(25);
            context.Employees.AddRange(data);
            context.SaveChanges();
        }
    }
}
