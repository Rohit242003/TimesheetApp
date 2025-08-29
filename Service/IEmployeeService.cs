using TimesheetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimesheetApp.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> RegisterAsync(Employee employee);
        Task<string?> LoginAsync(string email, string password);
        Task<Employee?> GetByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee?> UpdateEmployeeAsync(Employee employee); 
        Task<bool> DeleteEmployeeAsync(int id);
    }
}