using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class SiteType
    {
        [Key]
        public int SiteTypeID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Site Type")]
        public string SiteTypeName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}