using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class PMRDeficiency
    {
        [Key]
        public int PMRDeficiencyID { get; set; }

        public int PMRMasterID { get; set; }

        public int DeficiencyID { get; set; }

        public string Remarks { get; set; }


        public virtual PMRMaster PMRMasters { get; set; }
        public virtual PMRCheckList PMRCheckLists { get; set; }
    }
}