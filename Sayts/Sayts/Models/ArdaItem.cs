using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class ArdaItem
    {
        [Key]
        public int ArdaID { get; set; }

        public Categories ArdaCategory { get; set; }

        public int UnitID { get; set; }

        [StringLength(30)]
        public string Serial { get; set; }

        public int Quantity { get; set; }

        public string SiteID { get; set;}

        public string Sitename { get; set; }

        public int Cluster { get; set; }

        public string Province { get; set; }

        public string SiteAddress { get; set; }

        public string SiteOwner { get; set; }

        public string Contact { get; set; }

        public string Reason { get; set; }

        [Display(Name = "Original Location")]
        public string OriginalLocation { get; set; }

        [Display(Name = "Pickup Location")]
        public string PickUpLocation { get; set; }

        [Display(Name = "Staging Area")]
        public string StagingArea { get; set; }

        public Origins Origin { get; set; }

        [Display(Name = "RDF Control #")]
        public string RDFControl { get; set; }

        [Display(Name = "RDF Status")]
        public int ArdaStatusID { get; set; }

        public string Approver { get; set; }

        public Departments Department { get; set; }

        public string Remarks { get; set; }
        

        public virtual Unit Units { get; set; }
        public virtual ArdaStatus ArdaStatus { get; set; }

        public enum Origins
        {
            Smart, Sun
        }

        public enum Categories
        {
            Support_Facilities, Network_Build, Access, Transport, Others
        }

        public enum Departments
        {
            WATO, WFM, Finance, Logistics, Others
        }
    }
}