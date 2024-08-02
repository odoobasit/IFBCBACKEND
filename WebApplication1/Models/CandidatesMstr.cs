using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CandidatesMstr
    {
        [Key]
        public int DocId { get; set; }
        public DateTime? DocDate {  get; set; } = DateTime.Now; // Default to current date and time
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string territoryCity { get; set; }
        public string territoryState { get; set; }
        public string territoryZipcode { get; set; }
        public string currentCity { get; set; }
        public string currentState { get; set; }
        public string currentZipcode { get; set; }
        public string additionalFirstName { get; set; }   
        public string additionalLastName { get; set; }
        public string additionalEmail { get; set; }
        public string additionalPhone { get; set; }
        public string additionalRelationship { get; set; }
        public string franchiseInterested { get; set; }


    }
}
