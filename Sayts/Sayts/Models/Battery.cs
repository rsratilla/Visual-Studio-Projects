using System;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Battery
    {
        [Key]
        public int BatteryID { get; set; }

        public int SiteID { get; set; }

        public int UnitID { get; set; }

        public int StatusID { get; set; }

        [Display(Name = "Date Delivered")]
        public DateTime? DateDelivered { get; set; }

        [Display(Name = "Date Installed")]
        public DateTime DateInstalled { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime UpdateDate { get; set; }


        public virtual Site Sites { get; set; }
        public virtual Unit Units { get; set; }
        public virtual UnitStatus UnitStatus { get; set; }
    }
}