using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CandidateContacts
    {
        [Key]
        public int DocId { get; set; }
        public DateTime? DocDate {  get; set; } = DateTime.Now; // Default to current date and time
        public int CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RelationShip  { get; set; }

    }
}
