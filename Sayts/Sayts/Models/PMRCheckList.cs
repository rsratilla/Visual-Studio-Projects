using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class PMRCheckList
    {
        [Key]
        public int DeficiencyID { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public string Particular { get; set; }

        public string Note { get; set; }

        public string CLDefinition
        {
            get { return Type + " | " + Category + " | " + Particular; }
        }

        public string CLDisplay
        {
            get { return Category + " | " + Particular; }
        }

        public virtual ICollection<PMRDeficiency> PMRDeficiencies { get; set; }
    }
}