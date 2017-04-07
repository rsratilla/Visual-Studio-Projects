using System.ComponentModel.DataAnnotations;

namespace Sayts.Models
{
    public class Breaker
    {
        [Key]
        public int BreakerID { get; set; }

        public int RectifierID { get; set; }

        public int? Amp { get; set; }

        [StringLength(30)]
        public string F1 { get; set; }
        [StringLength(30)]
        public string F2 { get; set; }
        [StringLength(30)]
        public string F3 { get; set; }
        [StringLength(30)]
        public string F4 { get; set; }
        [StringLength(30)]
        public string F5 { get; set; }
        [StringLength(30)]
        public string F6 { get; set; }
        [StringLength(30)]
        public string F7 { get; set; }
        [StringLength(30)]
        public string F8 { get; set; }
        [StringLength(30)]
        public string F9 { get; set; }
        [StringLength(30)]
        public string F10 { get; set; }
        [StringLength(30)]
        public string F11 { get; set; }
        [StringLength(30)]
        public string F12 { get; set; }
        [StringLength(30)]
        public string F13 { get; set; }
        [StringLength(30)]
        public string F14 { get; set; }
        [StringLength(30)]
        public string F15 { get; set; }
        [StringLength(30)]
        public string F16 { get; set; }
        [StringLength(30)]
        public string F17 { get; set; }
        [StringLength(30)]
        public string F18 { get; set; }
        [StringLength(30)]
        public string F19 { get; set; }
        [StringLength(30)]
        public string F20 { get; set; }
        [StringLength(30)]
        public string F21 { get; set; }
        [StringLength(30)]
        public string F22 { get; set; }
        [StringLength(30)]
        public string F23 { get; set; }
        [StringLength(30)]
        public string F24 { get; set; }
        [StringLength(30)]
        public string F25 { get; set; }
        [StringLength(30)]
        public string F26 { get; set; }
        [StringLength(30)]
        public string F27 { get; set; }
        [StringLength(30)]
        public string F28 { get; set; }
        [StringLength(30)]
        public string F29 { get; set; }
        [StringLength(30)]
        public string F30 { get; set; }

        public virtual Rectifier Rectifiers { get; set; }
    }
}