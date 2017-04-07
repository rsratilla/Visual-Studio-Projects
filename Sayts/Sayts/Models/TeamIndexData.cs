using System.Collections.Generic;

namespace Sayts.Models
{
    public class TeamIndexData
    {
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<SubTeam> SubTeams { get; set; }
    }
}