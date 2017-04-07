using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Port
    {
        [Key]
        public int PortID { get; set; }

        public int SiteID { get; set; }

        [Required]
        public Facilities Facility { get; set; }

        [Required]
        public PortTypes PortType { get; set; }

        [Required]
        public int NoOfPorts { get; set; }
        
        public string StartPort { get; set; }


        public virtual Site Sites { get; set; }

        public virtual ICollection<PortInventory> PortInventories { get; set; }

        public enum PortTypes
        {
            E1, FE, Others
        }

        public enum Facilities
        {
            Nortel, ZTE, Tellabs, Huawei, ECI, BG, Tekelec, PLDT, Ericsson, ACM, OSS, Allied, Globe, FiberSwitch, Others, Etc
        }
    }
}