using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Site
    {
        [Key]
        public int SiteID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(20)]
        [Display(Name = "Site Code")]
        public string SiteCode { get; set; }

        [Required]
        [Display(Name = "Site Name")]
        public string SiteName { get; set; }

        [Display(Name = "Site Full Name")]
        public string SiteFullName
        {
            get { return SiteCode + " - " + SiteName; }
        }

        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
        public virtual ICollection<PMRMaster> PMRMasters { get; set; }
        public virtual ICollection<CMRTracker> CMRTrackers { get; set; }
        public virtual ICollection<Rectifier> Rectifiers { get; set; }
        public virtual ICollection<Battery> Batteries { get; set; }
        public virtual ICollection<ACU> ACUs { get; set; }
        public virtual ICollection<GenSet> GenSets { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }
        public virtual ICollection<Extinguisher> Extinguishers { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
        public virtual ICollection<Other> Others { get; set; }
        public virtual ICollection<Caretaker> Caretakers { get; set; }
    }
}