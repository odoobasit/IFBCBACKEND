using System.ComponentModel.DataAnnotations;

public class ListingsView
{
    [Key]
    public int? DocId { get; set; }
    public string? Name { get; set; }
    public string? ImgUrl { get; set; }
    public string? Category { get; set; }
    public string? InvestmentRange { get; set; }
    public string? TypeOfBusiness { get; set; }
    public string? ShortDescription { get; set; }
    public string? Documents { get; set; }
}
