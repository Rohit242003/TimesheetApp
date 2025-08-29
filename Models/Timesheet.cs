using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetApp.Models
{
    public class Timesheet
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 24, ErrorMessage = "Hours worked must be between 1 and 24")]
        public int HoursWorked { get; set; }

        [StringLength(500, ErrorMessage = "Task details cannot exceed 500 characters")]
        public string TaskDetails { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
