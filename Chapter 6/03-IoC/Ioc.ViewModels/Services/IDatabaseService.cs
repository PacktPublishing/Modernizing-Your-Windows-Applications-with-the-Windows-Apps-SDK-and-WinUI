using IoC.Models;

namespace IoC.Services
{
    public interface IDatabaseService
    {
        public void SavePerson(Person person);
    }
}
