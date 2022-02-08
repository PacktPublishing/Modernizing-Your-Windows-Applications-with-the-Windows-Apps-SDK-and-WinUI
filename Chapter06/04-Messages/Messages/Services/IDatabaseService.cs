using Messages.Models;

namespace Messages.Services
{
    public interface IDatabaseService
    {
        public void SavePerson(Person person);
    }
}
