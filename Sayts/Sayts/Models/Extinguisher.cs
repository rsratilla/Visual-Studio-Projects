using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Extinguisher
    {
        [Key]
        public int ExtinguisherID { get; set; }

        public int SiteID { get; set; }

        public int UnitID { get; set; }

        [Required]
        [StringLength(30)]
        public string Serial { get; set; }

        public int StatusID { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime UpdateDate { get; set; }


        public virtual Site Sites { get; set; }
        public virtual Unit Units { get; set; }
        public virtual UnitStatus UnitStatus { get; set; }
    }
}