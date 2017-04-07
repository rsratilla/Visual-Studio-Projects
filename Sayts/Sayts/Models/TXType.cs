using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class TXType
    {
        [Key]
        public int TXTypeID { get; set; }

        [Required]
        [StringLength(20)]
        public string TXTypeName { get; set; }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}