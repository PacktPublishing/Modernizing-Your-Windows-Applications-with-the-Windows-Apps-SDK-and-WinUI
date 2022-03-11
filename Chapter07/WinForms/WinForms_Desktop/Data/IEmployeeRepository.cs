using System.Linq;
using System.Threading.Tasks;

namespace WinForms_Desktop.Model
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllEmployees();
        Employee GetById(int id);
        void AddOrUpdate(Employee employee);
        void Delete(int id);
    }
}