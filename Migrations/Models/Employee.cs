using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMCrud.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }

        //[Column(TypeName = "Date")]
        //public DateTime DateOfJoining { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public int? ReportingManagerEmployeeId { get; set; }
        public string Gender { get; set; }
        public string EmployeeAddress { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Pincode { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
    }
}