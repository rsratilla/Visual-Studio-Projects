using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class ServiceCategory
    {
        [Key]
        public int ServiceCatID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Transport Category")]
        public string ServiceCat { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}