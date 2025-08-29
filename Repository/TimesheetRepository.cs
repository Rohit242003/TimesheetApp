using Microsoft.EntityFrameworkCore;
using TimesheetApp.Data;
using TimesheetApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.Repository
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly AppDbContext _context;

        public TimesheetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Timesheet>> GetAllAsync()
        {
            return await _context.Timesheets.Include(t => t.Employee).ToListAsync();
        }

        public async Task<Timesheet?> GetByIdAsync(int id)
        {
            return await _context.Timesheets.Include(t => t.Employee)
                                            .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Timesheet>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Timesheets
                                 .Where(t => t.EmployeeId == employeeId)
                                 .ToListAsync();
        }

        public async Task<Timesheet> AddAsync(Timesheet timesheet)
        {
            _context.Timesheets.Add(timesheet);
            await _context.SaveChangesAsync();
            return timesheet;
        }

        public async Task<Timesheet?> UpdateAsync(Timesheet timesheet)
        {
            var existing = await _context.Timesheets.FindAsync(timesheet.Id);
            if (existing == null) return null;

            existing.Date = timesheet.Date;
            existing.HoursWorked = timesheet.HoursWorked;
            existing.TaskDetails = timesheet.TaskDetails;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timesheet = await _context.Timesheets.FindAsync(id);
            if (timesheet == null) return false;

            _context.Timesheets.Remove(timesheet);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
