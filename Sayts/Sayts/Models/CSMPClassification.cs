using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class CSMPClassification
    {
        [Key]
        public int CSMPClassID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "CSMP Classification")]
        public string CSMPClass { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}