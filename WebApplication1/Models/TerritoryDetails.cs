using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TerritoryDetails
    {
        [Key]
        public int DocId { get; set; }
        public DateTime? DocDate {  get; set; } = DateTime.Now; // Default to current date and time
        public int ParentId { get; set; }
        public string Parent_Type { get; set; }
        public string TerritoryState { get; set; }
        public string TerritoryCity  { get; set; }        
        public string TerritoryZipCode  { get; set; }
        public string Territorynotes { get; set; }
        public bool IsPrimary { get; set; }

    }
}
