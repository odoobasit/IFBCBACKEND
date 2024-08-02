using System.ComponentModel.DataAnnotations;

public class listingsMstr
{
    [Key]
    public int? DocId { get; set; }
    public string? Category { get; set; }
    public string? Memberships { get; set; }
    public string? Name { get; set; }
    public string? FranchiseFee { get; set; }
    public string? ImgUrl { get; set; }
    public string? FranchisedUnits { get; set; }
    public string? OwnedUnits { get; set; }
    public string? ProjectedNewUnits { get; set; }
    public string? YearEstablished { get; set; }
    public string? TypeOfBusiness { get; set; }
    public string? Liquidity { get; set; }
    public string? InvestmentRange { get; set; }
    public string? MinimumNetWorth { get; set; }
    public string? MonthCash { get; set; }
    public string? RampUp { get; set; }
    public string shortdescription { get; set; }
    public string documents { get; set; }
    public string territories { get; set;}

}
