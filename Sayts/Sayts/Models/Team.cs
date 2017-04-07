using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [StringLength(40)]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        public virtual ICollection<SubTeam> SubTeams { get; set; }
    }
}