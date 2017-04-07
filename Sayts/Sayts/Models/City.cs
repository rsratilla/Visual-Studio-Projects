using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }

        [Display(Name = "City/Municipality")]
        [StringLength(40)]
        public string CityName { get; set; }

        [Display(Name = "Province")]
        [StringLength(40)]
        public string ProvinceName { get; set; }

        public int ClusterID { get; set; }

        public string CityFullName
        {
            get { return ClusterID + " - " + CityName; }
        }

        public string AreaClusterCities
        {
            get { return CityName + " (" + Clusters.ClusterName + ")"; }
        }

        public virtual Cluster Clusters { get; set; }
        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
        public virtual ICollection<Baranggay> Baranggays { get; set; }
    }
}