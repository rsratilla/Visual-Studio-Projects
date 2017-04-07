using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Cluster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        [Display(Name = "Cluster ID")]
        public int ClusterID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Cluster Name")]
        public string ClusterName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Area of Responsibility")]
        public string AORName { get; set; }

        public string ClusterFullName
        {
            get { return ClusterID + " - " + ClusterName ; }
        }

        public string AreaClusters
        {
            get { return ClusterName + " (" + Areas.AreaName + ")"; }
        }

        public int AreaID { get; set; }

        public virtual Area Areas { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ElectricCo> ElectricCos { get; set; }
        public virtual ICollection<SubBase> SubBases { get; set; }
        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
        public virtual ICollection<Decommission> Decommissions { get; set; }
    }
}