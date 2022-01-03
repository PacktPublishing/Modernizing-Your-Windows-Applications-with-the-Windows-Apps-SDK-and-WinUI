using Messages.Models;
using System.Diagnostics;

namespace Messages.Services
{
    public class DatabaseService : IDatabaseService
    {
        public void SavePerson(Person person)
        {
            Debug.WriteLine($"Person added: {person.Name} {person.Surname}");
        }
    }
}
