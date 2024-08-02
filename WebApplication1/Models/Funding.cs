using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Funding
    {
        [Key]
        public int DocId { get; set; }
        public DateTime DocDate { get; set; } = DateTime.Now; 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string FranchiseLocation { get; set; }
        public string DownPayment { get; set; }
        public string CreditScore { get; set; }
        public string Launching { get; set; }
        public string HouseHold { get; set; }
        public string DebtPayments { get; set; }
        public string CreditHistory { get; set; }
        public string Bankruptcies { get; set; }
        public string Percentage { get; set; }
        public string RealState { get; set; }
        public string TotalNet { get; set; }
    }
}
