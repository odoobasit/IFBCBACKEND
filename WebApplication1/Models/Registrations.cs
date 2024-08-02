using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Models
{
   
    public class Registrations
    {
        [Key]
        public int DocId { get; set; }
        public int CandidateId { get; set; }
        public int AgentId { get; set; }
        public string ListingsIds { get; set; }
        public Boolean InterRequest { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        [Required]
        public string DocType { get; set; }
        public string Email { get; set; } = "omerfarooqkhan7210@gmail.com"; // New email property with default value

        public DateTime DocDate { get; set; } = DateTime.Now; // Default to current date and time
        public string territoryState { get; set; }
        public string territoryCity { get; set; }
        public string territoryZipcode { get; set;}
        public bool isArchive { get; set; }
        public bool isRead { get; set; }
        public bool isFav { get; set; }
        public bool isTrash { get; set; }

    }
}
