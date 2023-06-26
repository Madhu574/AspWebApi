//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace EmployeeMCrud.Models
//{
//    [Table("Department")]
//    public class Department
//    {
//        [Key]
//        public int DepartmentId { get; set; }

//        public string DepartmentName { get; set; }
//    }
//}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMCrud.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Department name should only contain alphabets.")]
        [UniqueDepartmentName(ErrorMessage = "Department name should be unique.")]
        public string DepartmentName { get; set; }
    }

    public class UniqueDepartmentNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (APIDbContext)validationContext.GetService(typeof(APIDbContext));

            if (dbContext.Departments.Any(d => d.DepartmentName == (string)value))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
