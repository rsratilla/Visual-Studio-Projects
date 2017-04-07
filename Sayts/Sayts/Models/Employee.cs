using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sayts.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        [Display(Name = "Employee ID")]
        public int EmployeeNo { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "First Name")]
        public string GivenName { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public DateTime DateHired { get; set; }

        [StringLength(40)]
        [Display(Name = "Immediate Head")]
        public string ImmediateHead { get; set; }

        [StringLength(11, MinimumLength = 11)]
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }

        [StringLength(20)]
        [Display(Name = "Domain Account")]
        public string DomainAccount { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Office Address")]
        public string OfficeAddress { get; set; }

        public int? EmploymentStatusID { get; set; }

        public int? EmployeePositionID { get; set; }

        public int? SubTeamID { get; set; }

        [StringLength(40)]
        public string Remarks { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + GivenName + " " + MiddleName; }
        }

        public int? ClusterID { get; set; }

        public virtual Cluster Clusters { get; set; }
        public virtual SubTeam SubTeams { get; set; }
        public virtual EmployeePosition EmployeePositions { get; set; }
        public virtual EmploymentStatus EmploymentStatus { get; set; }
        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
        public virtual ICollection<PMRMaster> PMRMasters { get; set; }
    }
}