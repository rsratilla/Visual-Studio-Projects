using System.Collections.Generic;

namespace Sayts.Models
{
    public class InventoryIndexData
    {
        public IEnumerable<Site> Sites { get; set; }
        public IEnumerable<SiteDetail> SiteDetails { get; set; }
        public IEnumerable<ACU> ACUs { get; set; }
        public IEnumerable<Battery> Batteries { get; set; }
        public IEnumerable<Breaker> Breakers { get; set; }
        public IEnumerable<Extinguisher> Extinguishers { get; set; }
        public IEnumerable<GenSet> GenSets { get; set; }
        public IEnumerable<Rectifier> Rectifiers { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
        public IEnumerable<Port> Ports { get; set; }
        public IEnumerable<PortInventory> PortInventories { get; set; }
        public IEnumerable<Unit> Units { get; set; }
        public IEnumerable<UnitStatus> UnitStatus { get; set; }
        public IEnumerable<ArdaItem> ArdaItems { get; set; }
        public IEnumerable<ArdaStatus> ArdaStatus { get; set; }
        public IEnumerable<Other> Others { get; set; }
    }
}