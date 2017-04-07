using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class ElectricCo
    {
        [Key]
        public int ElectricCoID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Payee")]
        public string Payee { get; set; }

        [Display(Name = "Payee Type")]
        public string PayeeType { get; set; }

        [Display(Name = "Network")]
        public string Network { get; set; }

        public int ClusterID { get; set; }

        public virtual Cluster Clusters { get; set; }
    }
}