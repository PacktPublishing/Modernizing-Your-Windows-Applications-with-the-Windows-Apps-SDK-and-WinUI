using System.Collections.Generic;
using UWP_Desktop.Models;

namespace UWP_Desktop.Data
{
    public static class InitialSeed
    {
        public static IEnumerable<Employee> Seed(string locale, int count)
        {
            var faker = new EmployeeFaker(locale);
            return faker.Generate(count);
        }
    }
}
