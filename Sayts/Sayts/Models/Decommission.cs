using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Decommission
    {
        [Key]
        public int DecommID { get; set; }

        public int ClusterID { get; set; }

        [Required]
        public string SiteCode { get; set; }

        [Required]
        public string Sitename { get; set; }

        public DateTime DateDecom { get; set; }

        public string Services { get; set; }

        public string Reason { get; set; }


        public virtual Cluster Clusters { get; set; }
    }
}