using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sayts.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Sayts", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Sayts.Models.Area> Areas { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Cluster> Clusters { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.City> Cities { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Site> Sites { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Employee> Employees { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.EmployeePosition> EmployeePositions { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.EmploymentStatus> EmploymentStatus { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.SubBase> SubBases { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.SubTeam> SubTeams { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Team> Teams { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.ElectricCo> ElectricCos { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Region> Regions { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.SiteStatus> SiteStatus { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.SiteType> SiteTypes { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.SiteClassification> SiteClassifications { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.AssetOwnershipType> AssetOwnershipTypes { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.CSMPClassification> CSMPClassifications { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.TransportCategory> TransportCategories { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.ServiceCategory> ServiceCategories { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.LocationType> LocationTypes { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Terrain> Terrains { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Accessibility> Accessibilities { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.SecurityAndSafety> SecurityAndSafetys { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.ACMainSource> ACMainSources { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.SiteDetail> SiteDetails { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Baranggay> Baranggays { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.PMRMaster> PMRMasters { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.PMRDeficiency> PMRDeficiencies { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.PMRCheckList> PMRCheckLists { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.CMRReference> CMRReferences { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.CMRTracker> CMRTrackers { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Unit> Units { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.UnitStatus> UnitStatus { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Rectifier> Rectifiers { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Battery> Batteries { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.ACU> ACUs { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.GenSet> GenSets { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Shelter> Shelters { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Extinguisher> Extinguishers { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Breaker> Breakers { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Port> Ports { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.PortInventory> PortInventories { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.ArdaItem> ArdaItems { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.ArdaStatus> ArdaStatus { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Other> Others { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Caretaker> Caretakers { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.Decommission> Decommissions { get; set; }
        public System.Data.Entity.DbSet<Sayts.Models.TXType> TXTypes { get; set; }
    }
}