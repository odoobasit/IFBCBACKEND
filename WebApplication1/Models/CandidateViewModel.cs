using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class CandidateViewModel
    {
        [Key]
        public int DocId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TerritoryCity { get; set; }
        public string TerritoryState { get; set; }
        public string TerritoryZipcode { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public string CurrentZipcode { get; set; }
        public string TerritoryNotes { get; set; }
        public string FranchiseInterested { get; set; }
        public string Status { get; set; }
        public string PipelineStep { get; set; }
        public int AgentUserId { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
