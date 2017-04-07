using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sayts.Models
{
    public class InventorySiteGroup
    {
        public int? ClusterSite { get; set; }
        public int GSCount { get; set; }
        public int RectifierCount { get; set; }
        public int BatteryCount { get; set; }
        public int ACUCount { get; set; }
    }
}