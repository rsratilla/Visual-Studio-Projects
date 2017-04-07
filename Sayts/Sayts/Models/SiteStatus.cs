using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class SiteStatus
    {
        [Key]
        public int SiteStatusID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Site Status")]
        public string SiteStatusName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}