using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sayts.Models
{
    public class ArdaStatus
    {
        [Key]
        public int ArdaStatusID { get; set; }

        public string Status { get; set; }


        public virtual ICollection<ArdaItem> ArdaItem { get; set; }
    }
}