using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class AssetOwnershipType
    {
        [Key]
        public int AssetOwnershipID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Asset Ownership Type")]
        public string AssetOwnership { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}