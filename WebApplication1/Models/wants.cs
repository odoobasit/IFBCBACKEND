using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class wants
    {
        [Key]
        public int docid { get; set; }
        public string AttractiveBusinessOwner { get; set; }
        public string HandleNewBusiness { get; set; }
        public string BusinessExpectations { get; set; }
        public Boolean PreferB2b { get; set; }
        public Boolean PhysicalLocation { get; set; }
        public Boolean Inventory { get; set; }
        public Boolean ColdCalling { get; set; }
        public Boolean PassiveMode { get; set; }
        public Boolean BusinessHours { get; set; }
        public Boolean isCompleted { get; set; }


    }
}
