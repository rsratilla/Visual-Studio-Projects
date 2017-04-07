namespace Sayts.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sayts.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Sayts.Models.ApplicationDbContext";
        }

        protected override void Seed(Sayts.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.CMRReferences.AddOrUpdate(
             //     c => c.CMRRefID,
              //      new CMRReference { RepairWork = "Hauling ", SLA = 1, WODependecy = "WFM" },
              //      new CMRReference { RepairWork = "Battery", SLA = 1, WODependecy = "WATO" },
              //      new CMRReference { RepairWork = "Change Oil", SLA = 1, WODependecy = "WATO" },
              //      new CMRReference { RepairWork = "Air/Oil Filter", SLA = 1, WODependecy = "WATO" },
              //      new CMRReference { RepairWork = "RH Meter", SLA = 1, WODependecy = "WATO" },
              //      new CMRReference { RepairWork = "Battery Switch/terminal", SLA = 1, WODependecy = "WATO" },
              //      new CMRReference { RepairWork = "Coolant", SLA = 1, WODependecy = "WATO" },
              //      new CMRReference { RepairWork = "Minor Accessories Repair", SLA = 1, WODependecy = "WATO" },
              //      new CMRReference { RepairWork = "Others", SLA = 1, WODependecy = "WATO" }
            //);
        }
    }
}
