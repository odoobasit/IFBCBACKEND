using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ContactUs
    {
        [Key]
        public int ContactId { get; set; }

        public DateTime? ContactDate {  get; set; } = DateTime.Now; // Default to current date and time
        public string ContactReason { get; set; }
        public string ContactName { get; set; }
        public string ContactCompany { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactComments { get; set; }       
        public bool ContactCopy { get; set; }
        public string ContactPath { get; set; }


    }
}
