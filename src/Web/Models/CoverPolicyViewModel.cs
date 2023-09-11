using System.ComponentModel;

namespace Web.Models
{
    public class CoverPolicyViewModel
    {
        [DisplayName("Cover")]
        public string? CoverType { get; set; }

        [DisplayName("Period")]
        public string? PeriodType { get; set; }

        [DisplayName("Duration")]
        public int People { get; set; } = 0;
        public int Rate { get; set; } = 1;

        [DisplayName("Estimate")]
        public decimal Cost { get; set; } = 0;
    }
}