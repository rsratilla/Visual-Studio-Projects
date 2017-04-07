using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class EmployeePosition
    {
        [Key]
        public int EmployeePositionID { get; set; }

        [StringLength(40)]
        [Display(Name = "Employee Position")]
        public string EmployeePositionName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}