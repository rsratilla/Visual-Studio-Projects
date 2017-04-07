using System;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Shelter
    {
        [Key]
        public int ShelterID { get; set; }

        public int SiteID { get; set; }

        public int UnitID { get; set; }

        public int StatusID { get; set; }
        
        public string Remarks { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime UpdateDate { get; set; }


        public virtual Site Sites { get; set; }
        public virtual Unit Units { get; set; }
        public virtual UnitStatus UnitStatus { get; set; }
    }
}