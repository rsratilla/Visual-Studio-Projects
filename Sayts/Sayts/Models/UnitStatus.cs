using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class UnitStatus
    {
        [Key]
        public int StatusID { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public virtual ICollection<Rectifier> Rectifiers { get; set; }
        public virtual ICollection<Battery> Batteries { get; set; }
        public virtual ICollection<ACU> ACUs { get; set; }
        public virtual ICollection<GenSet> GenSets { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }              
    }
}