using Microsoft.AspNetCore.Mvc;
using TimesheetApp.Models;
using TimesheetApp.Services;

namespace TimesheetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public AuthController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Employee employee)
        {
            if (employee.Role == "Admin" && !User.IsInRole("Admin"))
                return Forbid("Only Admin can create Admin users.");

            var result = await _employeeService.RegisterAsync(employee);
            if (result == null) return BadRequest("Employee already exists");
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var employee = await _employeeService.GetByEmailAsync(request.Email);
            if (employee == null)
                return Unauthorized("Invalid credentials");

            var token = await _employeeService.LoginAsync(request.Email, request.Password);
            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new
            {
                token,
                role = employee.Role,
                id = employee.Id
            });
        }
    }
}