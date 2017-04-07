using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class GenSet
    {
        [Key]
        public int GenSetID { get; set; }

        public int SiteID { get; set; }

        [Required]
        [StringLength(30)]
        public string Serial { get; set; }

        public int UnitID { get; set; }
        
        public decimal Capacity { get; set; }
        
        public int StatusID { get; set; }

        [Display(Name = "Date Delivered")]
        public DateTime? DateDelivered { get; set; }

        [Display(Name = "Date Installed")]
        public DateTime DateInstalled { get; set; }
               
        [Display(Name = "AC Loading")]
        public int? ACLoading { get; set; }
                
        public string Remarks { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime UpdateDate { get; set; }


        public virtual Site Sites { get; set; }
        public virtual Unit Units { get; set; }
        public virtual UnitStatus UnitStatus { get; set; }
    }
}