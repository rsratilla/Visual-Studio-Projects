using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class TransportCategory
    {
        [Key]
        public int TransportCatID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Transport Category")]
        public string TransportCat { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}