using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Area
    {
        [Key]
        public int AreaID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        public virtual ICollection<Cluster> Clusters { get; set; }
        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}