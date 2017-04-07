using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class SecurityAndSafety
    {
        [Key]
        public int SASID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Security And Safety")]
        public string SASName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}