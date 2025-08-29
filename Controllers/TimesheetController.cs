using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Models;
using TimesheetApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimesheetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTimesheet(Timesheet timesheet)
        {
            var result = await _timesheetService.AddAsync(timesheet);
            return CreatedAtAction(nameof(GetTimesheets), new { employeeId = result.EmployeeId }, result);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetTimesheets(int employeeId)
        {
            var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Admin" && loggedInUserId != employeeId)
            {
                
                return StatusCode(403, "You are not authorized to view this data.");
            }

            var timesheets = await _timesheetService.GetByEmployeeIdAsync(employeeId);
            return Ok(timesheets);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimesheet(int id, Timesheet timesheet)
        {
            if (id != timesheet.Id)
            {
                return BadRequest("Timesheet ID mismatch.");
            }

            var existingTimesheet = await _timesheetService.GetByIdAsync(id);
            if (existingTimesheet == null)
            {
                return NotFound();
            }

            var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Admin" && loggedInUserId != existingTimesheet.EmployeeId)
            {
               
                return StatusCode(403, "You are not authorized to modify this timesheet.");
            }

            var updatedTimesheet = await _timesheetService.UpdateAsync(timesheet);
            if (updatedTimesheet == null)
            {
                return NotFound();
            }

            return Ok("Timesheet updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimesheet(int id)
        {
            var timesheet = await _timesheetService.GetByIdAsync(id);
            if (timesheet == null)
            {
                return NotFound();
            }

            var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Admin" && loggedInUserId != timesheet.EmployeeId)
            {
               
                return StatusCode(403, "You are not authorized to delete this timesheet.");
            }

            var deleted = await _timesheetService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok("Timesheet deleted successfully.");
        }
    }
}