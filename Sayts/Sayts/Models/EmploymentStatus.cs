using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class EmploymentStatus
    {
        [Key]
        public int EmploymentStatusID { get; set; }

        [StringLength(40)]
        [Display(Name = "Employment Status")]
        public string EmploymentStatusName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}