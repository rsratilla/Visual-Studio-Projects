using System.Collections.Generic;

namespace Sayts.Models
{
    public class AreaIndexData
    {
        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<Cluster> Clusters { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<SubBase> SubBases { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<ElectricCo> ElectricCos { get; set; }
        public IEnumerable<Baranggay> Baranggays { get; set; }
        public IEnumerable<Port> Ports { get; set; }
        public IEnumerable<PortInventory> PortInventories { get; set; }
    }
}