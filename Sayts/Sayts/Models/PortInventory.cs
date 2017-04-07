using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class PortInventory
    {
        [Key]
        public int PortInventoryID { get; set; }

        public int PortID { get; set; }

        [StringLength(20)]
        public string Port { get; set; }

        [Required]
        [Display(Name = "Connector Type")]
        public ConnectorTypes ConnectorType { get; set; }

        [Display(Name = "Circuit Assignment")]
        public string CircuitAssignment { get; set; }

        public int? Bandwidth { get; set; }

        [Display(Name = "Neighbor Port")]
        public string NeighborPort { get; set; }

        public string Remarks { get; set; }


        public virtual Port Ports { get; set; }

        public enum ConnectorTypes
        {
            Electrical, Optical_SM, Optical_MM, Others
        }
    }
}