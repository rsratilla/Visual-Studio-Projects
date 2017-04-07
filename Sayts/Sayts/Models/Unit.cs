using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Unit
    {
        [Key]
        [Column(Order = 1)]
        public int UnitID { get; set; }

        [Required]
        [StringLength(20)]
        public string Category { get; set; }
                
        [Required]
        [Display(Name = "Brand/Model")]
        public string BrandModel { get; set; }

        public string Type { get; set; }

        public string Capacity { get; set; }

		public string BrandType
        {
            get { return BrandModel + " " + Type; }
        }
        
        public string CategoryBrand
        {
            get { return Category + "-" + BrandModel; }
        }
		
        public virtual ICollection<CMRTracker> CMRTrackers { get; set; }
        public virtual ICollection<Rectifier> Rectifiers { get; set; }
        public virtual ICollection<Battery> Batteries { get; set; }
        public virtual ICollection<ACU> ACUs { get; set; }
        public virtual ICollection<GenSet> GenSets { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }
        
        public ICollection<ArdaItem> ArdaItems { get; set; }
    }
}