using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class eligibility
    {
        [Key]
        public int docid { get; set; }
        public string VALoan { get; set; }
        public string EligibilityValue { get; set; }        
        public string TrafficViolation { get; set; }
        public string Unsatisfiedjudgment { get; set; }
        public string Bankruptcy { get; set; }
        public Boolean isCompleted { get; set; }


    }
}
