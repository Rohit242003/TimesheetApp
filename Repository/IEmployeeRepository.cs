using TimesheetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimesheetApp.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee?> Register(Employee employee);
        Task<Employee?> Login(string email, string password); 
        Task<Employee?> GetByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}