using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDemoProject.Models
{
    public class Designation
    {
   
        [Key]
        public string DesignationName { get; set; }

    }
}
