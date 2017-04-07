using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class CMRReference
    {
        [Key]
        public int CMRRefID { get; set; }

        [Required]
        [Display(Name = "Repair Work")]
        public string RepairWork { get; set; }

        [Display(Name = "SLA")]
        public int? SLA { get; set; }

        [Display(Name = "Warranty")]
        public string Warranty { get; set; }

        [Display(Name = "WO Dependency")]
        public string WODependecy { get; set; }

        public virtual ICollection<CMRTracker> CMRTrackers { get; set; }
    }
}