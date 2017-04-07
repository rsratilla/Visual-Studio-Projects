using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Terrain
    {
        [Key]
        public int TerrainID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Terrain")]
        public string TerrainName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}