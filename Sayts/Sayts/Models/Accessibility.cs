using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Accessibility
    {
        [Key]
        public int AccessibilityID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Accessibility")]
        public string AccessibilityName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}