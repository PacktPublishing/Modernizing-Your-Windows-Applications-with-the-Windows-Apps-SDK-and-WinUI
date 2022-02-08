using Navigation.Models;

namespace Navigation.Contracts
{
    public interface IDatabaseService
    {
        public void SavePerson(Person person);
    }
}
