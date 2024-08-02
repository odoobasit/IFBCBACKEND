using System.ComponentModel.DataAnnotations;

public class listingsOwner
{
    [Key]
    public int? DocId { get; set; }
    public string? Website { get; set; }
    public string? Username { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? UpdatedInfo { get; set; }
    public string? FddYearInfo { get; set; }
    public string? AvgResponseTime { get; set; }
    public string? FranchisedUnits { get; set; }
    public string? ImgUrl { get; set; }
    public string? name { get; set;}
   
}
