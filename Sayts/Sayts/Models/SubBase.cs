using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class SubBase
    {
        [Key]
        public int SubBaseID { get; set; }

        [Required]
        [Display(Name = "Sub Base")]
        [StringLength(20)]
        public string SubBaseName { get; set; }

        public int ClusterID { get; set; }


        public virtual Cluster Clusters { get; set; }
    }
}