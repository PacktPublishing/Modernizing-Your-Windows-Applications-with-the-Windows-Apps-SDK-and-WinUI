using System.Collections.Generic;
using System.Threading.Tasks;
using ModernDesktop.Model;

namespace ModernDesktop.Contracts
{
    public interface IDataService
    {
        IEnumerable<Employee> GetAll();
        Task<Employee> GetByIdAsync(int id);
        Task UpsertAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}

