using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class FranchiseIssue
    {
        [Key]
        public int DocId { get; set; }

        public DateTime? DocDate {  get; set; } = DateTime.Now; // Default to current date and time
        public int listingId { get; set; }
        public string listingEmail { get; set; }
        public string listingName { get; set; }
        public string issueListing { get; set; }
        public string issue { get; set; }
        

    }
}
