using System.ComponentModel.DataAnnotations;
using YourNamespace.Models;

namespace WebApplication1.Models
{
    public class initialqualify
    {
        [Key]
        public int docid { get; set; }
        public string Funding { get; set; }
        public string InvestmentFranchise { get; set; }
        public string CreditScore { get; set; }
        public string Networth { get; set; }
        public string LiquidCash { get; set; }
        public string FranchiseCause { get; set; }
        public string ProfessionalBackground { get; set; }
        public string TimeFrame { get; set; }
        public Boolean isCompleted { get; set; }


    }
}
