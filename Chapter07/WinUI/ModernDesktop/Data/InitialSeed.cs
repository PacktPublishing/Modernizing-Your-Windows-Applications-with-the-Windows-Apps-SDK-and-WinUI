using System.Collections.Generic;
using ModernDesktop.Model;

namespace ModernDesktop.Data
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
