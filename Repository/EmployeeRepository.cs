using Microsoft.EntityFrameworkCore;
using TimesheetApp.Data;
using TimesheetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimesheetApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> Register(Employee employee)
        {
            if (await _context.Employees.AnyAsync(e => e.Email == employee.Email))
                return null;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

       
        public async Task<Employee?> Login(string email, string password)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

       
        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            var existing = await _context.Employees.FindAsync(employee.Id);
            if (existing == null) return null;

            existing.Name = employee.Name;
            existing.Email = employee.Email;
            existing.Password = employee.Password; 
            existing.Role = employee.Role;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}