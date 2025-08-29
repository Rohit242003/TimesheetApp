using TimesheetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimesheetApp.Repository
{
    public interface ITimesheetRepository
    {
        Task<IEnumerable<Timesheet>> GetAllAsync();
        Task<Timesheet?> GetByIdAsync(int id);
        Task<IEnumerable<Timesheet>> GetByEmployeeIdAsync(int employeeId);
        Task<Timesheet> AddAsync(Timesheet timesheet);
        Task<Timesheet?> UpdateAsync(Timesheet timesheet);
        Task<bool> DeleteAsync(int id);
    }
}
