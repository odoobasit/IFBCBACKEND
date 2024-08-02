using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Emailmstr
    {
        [Key]
        public int DocId { get; set; }
        public DateTime? DocDate {  get; set; } = DateTime.Now; // Default to current date and time
        public int AgentId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int FranchisedId { get; set; }        
        public bool IsArchive { get; set; }
        public bool IsRead { get; set; }
        public bool IsFav { get; set; }
        public bool IsTrash { get; set; }
    }
}
