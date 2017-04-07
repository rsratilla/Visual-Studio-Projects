using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class PMRMaster
    {
        [Key]
        public int PMRMasterID { get; set; }

        [Required]
        [Display(Name = "PMR Date")]
        public DateTime PMRDate { get; set; }

        public int SiteID { get; set; }

        public int EmployeeNo { get; set; }

        [Required]
        [Display(Name = "Site Classification")]
        public SiteClass SiteClassification { get; set; }

        [Required]
        [Display(Name = "Type")]
        public PMRTypes PMRType { get; set; }

        [Required]
        [Display(Name = "W/ GS?")]
        public bool WithGS { get; set; }

        public string CoordinationNo { get; set; }

        public string Lilo { get; set; }

        [Required]
        [Display(Name = "Mains Simulation Status")]
        public MainsSimul? MainsSimulStatus  { get; set; }

        [Display(Name = "Mains Simulation Remarks")]
        public string MainsSimulRemarks { get; set; }

        public string Remarks { get; set; }


        public enum SiteClass
        {
            A,B,C
        }

        public enum MainsSimul
        {
            Passed, Failed, NotApplicable
        }

        public enum PMRTypes
        {
            Intensive, Quarterly
        }
            
        public virtual Site Sites { get; set; }
        public virtual Employee Employees { get; set; }
        public virtual ICollection<PMRDeficiency> PMRDeficiencies { get; set; }
    }
}