using System;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Caretaker
    {
        public int CaretakerID { get; set; }

        public int SiteID { get; set; }

        public string Agency { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Name of Caretaker")]
        public string CTName { get; set; }

        public DateTime EffectivityDate { get; set; }

        public string ContactNo { get; set; }


        public string Remarks { get; set; }

        public virtual Site Sites { get; set; }
    }
}