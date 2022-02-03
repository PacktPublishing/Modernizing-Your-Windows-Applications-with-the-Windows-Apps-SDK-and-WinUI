using System.Collections.Generic;
using System.Threading.Tasks;
using UWP_Desktop.Models;

namespace UWP_Desktop.Contracts
{
    public interface IDataService
    {
        IEnumerable<Employee> GetAll();
        Task<Employee> GetByIdAsync(int id);
        Task UpsertAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}

