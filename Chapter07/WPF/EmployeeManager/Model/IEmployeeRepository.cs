using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Model
{
    /// <summary>
    ///     Sample for a repository that is modelled with async operations.
    ///     This should usually be the case since most of the time when you communicate
    ///     with a repository you are doing some I/O (database, file, ...).
    /// </summary>
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Query();
        Task<Employee> AddAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task UpdateAsync(Employee employee);
    }
}