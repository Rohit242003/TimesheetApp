using TimesheetApp.Models;
using TimesheetApp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimesheetApp.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ITimesheetRepository _timesheetRepository;

        public TimesheetService(ITimesheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }

        public Task<IEnumerable<Timesheet>> GetAllAsync()
        {
            return _timesheetRepository.GetAllAsync();
        }

        public Task<Timesheet?> GetByIdAsync(int id)
        {
            return _timesheetRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Timesheet>> GetByEmployeeIdAsync(int employeeId)
        {
            return _timesheetRepository.GetByEmployeeIdAsync(employeeId);
        }

        public Task<Timesheet> AddAsync(Timesheet timesheet)
        {
            return _timesheetRepository.AddAsync(timesheet);
        }

        public Task<Timesheet?> UpdateAsync(Timesheet timesheet)
        {
            return _timesheetRepository.UpdateAsync(timesheet);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _timesheetRepository.DeleteAsync(id);
        }
    }
}
