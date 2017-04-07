using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Region
    {
        [Key]
        [Required]
        [StringLength(15)]
        [Display(Name = "Region ID")]
        public string RegionID { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; }

        public string RegionFullName
        {
            get { return RegionID + " - " + RegionName; }
        }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}