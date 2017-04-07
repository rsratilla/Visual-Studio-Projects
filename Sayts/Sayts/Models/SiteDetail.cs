using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class SiteDetail
    {
        [Key]
        public int SiteDetailID { get; set; }

        [Index(IsUnique = true)]
        public int SiteID { get; set; }

        public int? EmployeeNo { get; set; }

        [Display(Name = "No/Bldg/Lot/Street")]
        public string HouseLotNo { get; set; }

        public int? AreaID { get; set; }

        public int? ClusterID { get; set; }

        public string RegionID { get; set; }

        public int? CityID { get; set; }

        public int? BarangayID { get; set; }

        public string Longitude { get; set; }

        public string Lattitude { get; set; }

        public int? SiteStatusID { get; set; }

        public int? SiteTypeID { get; set; }

        public int? SiteClassificationID { get; set; }

        public int? AssetOwnershipID { get; set; }

        public int? CSMPClassID { get; set; }

        public int? TransportCatID { get; set; }

        public int? ServiceCatID { get; set; }

        public int? LocationTypeID { get; set; }

        public int? TerrainID { get; set; }

        public int? AccessibilityID { get; set; }

        public int? SASID { get; set; }

        public int? ACMID { get; set; }

        public string SunID { get; set; }

        public string SubBase { get; set; }

        public int? TXTypeID { get; set; }

        public string AccessIssue { get; set; }

        public string TimeOfIssue { get; set; }

        public string RiskCategory { get; set; }

        [Display(Name = "Travel Time from Base to Site")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime? TravelTime { get; set; }

        [Display(Name = "Access Pass Processing Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime? AccessPassTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        [Display(Name = "Monthyl Average Revenue")]
        public decimal? MonthlyRevenue { get; set; }

        [Display(Name = "Site For CSMP & WATO PMR")]
        public bool ForCSMPPMR { get; set; }

        public string Remarks { get; set; }


        public virtual Site Sites { get; set; }
        public virtual Employee Employees { get; set; }
        public virtual City Cities { get; set; }
        public virtual Area Areas { get; set; }
        public virtual Region Regions { get; set; }
        public virtual Cluster Clusters { get; set; }
        public virtual SiteStatus SiteStatus { get; set; }
        public virtual SiteType SiteTypes { get; set; }
        public virtual SiteClassification SiteClassifications { get; set; }
        public virtual AssetOwnershipType AssetOwnershipTypes { get; set; }
        public virtual CSMPClassification CSMPClassifications { get; set; }
        public virtual TransportCategory TransportCategories { get; set; }
        public virtual ServiceCategory ServiceCategories { get; set; }
        public virtual LocationType LocationTypes { get; set; }
        public virtual Terrain Terrains { get; set; }
        public virtual Accessibility Accessibilities { get; set; }
        public virtual SecurityAndSafety SecurityAndSafetys { get; set; }
        public virtual ACMainSource ACMainSources { get; set; }
        public virtual Baranggay Baranggays { get; set; }
        public virtual TXType TXTypes { get; set; }
    }
}