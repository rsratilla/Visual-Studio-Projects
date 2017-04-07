using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Other
    {
        [Key]
        public int OthersID { get; set; }

        public int SiteID { get; set; }

        [Display(Name = "ATS Serial")]
        public string ATSSerial { get; set; }

        [Display(Name = "ATS Type")]
        public string ATSType { get; set; }

        [Display(Name = "ATS Status")]
        [StringLength(30)]
        public string ATSStatus { get; set; }

        [Display(Name = "Date Delivered")]
        public DateTime DateDelivered { get; set; }

        [Display(Name = "Date Installed")]
        public DateTime DateInstalled { get; set; }

        [Display(Name = "With Cranking Module")]
        public Boolean? HasCrankModule { get; set; }

        [Display(Name = "With Battery Charger")]
        public Boolean? HasBatCharger { get; set; }

        [Display(Name = "ATS Remarks")]
        public string ATSRemarks { get; set; }

        [Display(Name = "Fuel Tank Status")]
        [StringLength(30)]
        public string FTStatus { get; set; }

        [Display(Name = "Fuel Tank Capacity")]
        public int? FTCapacity { get; set; }

        [Display(Name = "Fuel Tank Remarks")]
        public string FTRemarks { get; set; }

        [Display(Name = "Fuel Line Status")]
        [StringLength(30)]
        public string FLStatus { get; set; }

        [Display(Name = "Fuel Line Remarks")]
        public string FLRemarks { get; set; }

        [Display(Name = "TVSS Serial")]
        public string TVSSerial { get; set; }

        [Display(Name = "TVSS Type")]
        public TVSSTypes? TVSSType { get; set; }

        [Display(Name = "TVSS Status")]
        [StringLength(30)]
        public string TVSSStatus { get; set; }

        [Display(Name = "TVSS Remarks")]
        public string TVSSRemarks { get; set; }

        [Display(Name = "Exhaust Fan Status")]
        [StringLength(30)]
        public string EFStatus { get; set; }

        [Display(Name = "Exhaust Fan Remarks")]
        public string EFRemarks { get; set; }

        [Display(Name = "Tower Type")]
        public string ToType { get; set; }

        [Display(Name = "Tower Height")]
        public int? ToHeight { get; set; }

        [Display(Name = "Tower Status")]
        [StringLength(30)]
        public string ToStatus { get; set; }

        [Display(Name = "Tower Remarks")]
        public string ToRemarks { get; set; }

        [Display(Name = "Tower Light Status")]
        [StringLength(30)]
        public string TLStatus { get; set; }

        [Display(Name = "Tower Light Remarks")]
        public string TLRemarks { get; set; }

        [Display(Name = "Fence Remarks")]
        public string FenceRemarks { get; set; }

        [Display(Name = "Lightning Arrester Status")]
        [StringLength(30)]
        public string LAStatus { get; set; }

        [Display(Name = "Bus Bar Temination Status")]
        [StringLength(30)]
        public string BBStatus { get; set; }

        [Display(Name = "Tower Grounding Status")]
        [StringLength(30)]
        public string TGStatus { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime UpdateDate { get; set; }


        public virtual Site Sites { get; set; }

        public enum TVSSTypes
        {
            Indoor, Outdoor, Others
        }
    }
}