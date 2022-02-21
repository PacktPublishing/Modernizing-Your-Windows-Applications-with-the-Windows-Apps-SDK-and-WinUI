using Navigation.Contracts;
using Navigation.Models;
using System.Diagnostics;

namespace Navigation.Services
{
    public class DatabaseService : IDatabaseService
    {
        public void SavePerson(Person person)
        {
            Debug.WriteLine($"Person added: {person.Name} {person.Surname}");
        }
    }
}
