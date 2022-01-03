using IoC.Models;
using System.Diagnostics;

namespace IoC.Services
{
    public class DatabaseService : IDatabaseService
    {
        public void SavePerson(Person person)
        {
            Debug.WriteLine($"Person added: {person.Name} {person.Surname}");
        }
    }
}
