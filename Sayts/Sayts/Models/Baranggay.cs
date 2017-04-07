using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Baranggay
    {
        [Key]
        public int BarangayID { get; set; }

        [Required]
        [Display(Name = "Baranggay")]
        [StringLength(50)]
        public string BarangayName { get; set; }

        public int CityID { get; set; }

        public string AreaClusterCityBrgy
        {
            get { return BarangayName + " (" + Cities.AreaClusterCities + ")"; }
        }

        public virtual City Cities { get; set; }
    }
}