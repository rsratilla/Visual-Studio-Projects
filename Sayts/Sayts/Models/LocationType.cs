using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class LocationType
    {
        [Key]
        public int LocationTypeID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Location Type")]
        public string LocationTypeDescription { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}