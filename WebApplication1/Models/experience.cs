using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class experience
    {
        [Key]
        public int docid { get; set; }
        public string BusinessBefore { get; set; }
        public string MarketingExperience { get; set; }
        public string ManagementExperience { get; set; }
        public string SalesExperience { get; set; }
        public string ReviewFinancialStatement { get; set; }
        public string CSExperience { get; set; }
        public Boolean isCompleted { get; set; }

    }
}
