using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class ACMainSource
    {
        [Key]
        public int ACMID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "AC MainSource")]
        public string ACMName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}