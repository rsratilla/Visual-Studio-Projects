using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Rectifier
    {
        [Key]
        public int RectifierID { get; set; }

        public int SiteID { get; set; }

        [Required]
        [StringLength(30)]
        public string Serial { get; set; }

        public int UnitID { get; set; }

        public int StatusID { get; set; }

        [Display(Name = "Date Delivered")]
        public DateTime? DateDelivered { get; set; }

        [Display(Name = "Date Installed")]
        public DateTime DateInstalled { get; set; }

        [Required]
        public DCPPTypes DCPPType { get; set; }

        [Display(Name = "# of Active RMs")]
        public int? RMActive { get; set; }

        [Display(Name = "# of Defective RMs")]
        public int? RMDefective { get; set; }

        public decimal? Voltage { get; set; }

        [Display(Name = "Battery Current")]
        public decimal? BatteryCurr { get; set; }

        [Display(Name = "Rectifier Current")]
        public decimal? RectifierCurr { get; set; }

        [Display(Name = "DC Loading (amps)")]
        public decimal DCLoading { get; set; }

        [Display(Name = "Backup Time")]
        public decimal? TimeBackUp { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime UpdateDate { get; set; }

        public enum DCPPTypes
        {
            Indoor, Outdoor, Others
        }

        public virtual Site Sites { get; set; }
        public virtual Unit Units { get; set; }
        public virtual UnitStatus UnitStatus { get; set; }

        public virtual ICollection<Breaker> Breakers { get; set; }
    }
}