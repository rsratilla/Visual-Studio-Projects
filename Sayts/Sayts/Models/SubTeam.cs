using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class SubTeam
    {
        [Key]
        public int SubTeamID { get; set; }

        [StringLength(40)]
        [Display(Name = "Sub-Team Name")]
        public string SubTeamName { get; set; }

        public int TeamID { get; set; }

        public virtual Team Teams { get; set; }
    }
}