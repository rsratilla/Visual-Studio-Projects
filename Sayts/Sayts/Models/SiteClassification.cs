using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class SiteClassification
    {
        [Key]
        public int SiteClassificationID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Site Classification")]
        public string SiteClassificationName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}