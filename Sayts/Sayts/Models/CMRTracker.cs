using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class CMRTracker
    {
        [Key]
        [Column(Order = 1)]
        public int CMRTrackerID { get; set; }

        public int SiteID { get; set; }

        [Required]
        [Display(Name = "CMR Source")]
        public CMRSources CMRSource { get; set; }

        [Display(Name = "Ecall Ticket No")]
        public int? EcallNo { get; set; }

        public string Issue { get; set; }

        [Display(Name = "Involve Groups")]
        public bool CSMPGroup { get; set; }
        public bool WatoGroup { get; set; }
        public bool WFMGroup { get; set; }

        [Display(Name = "Date Endorsed")]
        public DateTime CMRDateEndorsed { get; set; }
        
        [Display(Name = "Status")]
        public CMRStatus CMRStats { get; set; }

        [Display(Name = "Status")]
        public CMRStatus FSRStats { get; set; }

        [Display(Name = "Date Endorsed")]
        public DateTime FSRDateEndorsed { get; set; }

        [Display(Name = "Date Endorsed")]
        public DateTime QouDateEndorsed { get; set; }

        [Required]
        [Display(Name = "Consumbale Category")]
        public Consumables Consumable { get; set; }

        [Display(Name = "Brand/Model")]
        public int? UnitID { get; set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        public string Serial { get; set; }

        [Required]
        [Display(Name = "Repair Works / Consumables")]
        public int? CMRRefID { get; set; }

        [Display(Name = "WO No")]
        public int? WoNo { get; set; }
        
        [Display(Name = "Date Filed")]
        public DateTime WODateFiled { get; set; }

        [Display(Name = "Status")]
        public WOStatus WOStat { get; set; }

        [Display(Name = "Date Implemented")]
        public DateTime WODateImplemented { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Date Rectified")]
        public DateTime DateRectified { get; set; }

        public enum CMRSources
        {
            PMR, ECALL
        }
        public enum CMRStatus
        {
            Pending, Done
        }
        public enum Consumables
        {
            Genset, ATS, ACU
        }
        public enum WOStatus
        {
            [Display(Name = "For Level1 Approval")]
            L1,
            [Display(Name = "For Level2 Approval")]
            L2,
            [Display(Name = "For Implementation")]
            FI
        }
        public enum FSRStatus
        {
            [Display(Name = "Awaiting from CSMP")]
            AC,
            [Display(Name = "Submitted to WFM")]
            SW,
            [Display(Name = "Wato On-Hand")]
            WH
        }


        public virtual CMRReference CMRReferences { get; set; }
        public virtual Site Sites { get; set; }
        public virtual Unit Units { get; set; }
    }
}