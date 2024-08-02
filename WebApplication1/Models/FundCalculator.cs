using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace WebApplication1.Models
{
    public class FundCalculator
    {
        [Key]
        public int DocId { get; set; }
        public DateTime? DocDate {  get; set; } = DateTime.Now; // Default to current date and time
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FranchiseLocation { get; set; }
        public int DownPayment { get; set; }
        public string CreditScore { get; set; }
        public string Launching {  get; set; }     
        public int HouseHold { get; set; }
        public int DebtPayments { get; set; }
        public string CreditHistory { get; set; }
        public string Bankruptcies { get; set; }
        public string Percentage { get; set; }
        public string RealState { get; set; }
        public int TotalNet { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
