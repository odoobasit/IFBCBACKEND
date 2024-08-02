using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ListingsContent
    {
        [Key]
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        
    }
}
