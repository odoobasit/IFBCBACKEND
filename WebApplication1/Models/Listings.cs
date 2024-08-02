using System.ComponentModel.DataAnnotations;

public class Listings
{
    [Key]
    public int? DocId { get; set; }
    public string? Name { get; set; }
    public string? SmallDesc { get; set; }
    public string? UpdatedInfo { get; set; }
    public string? FddYearInfo { get; set; }
    public string? Category { get; set; }
    public string? Username { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? ImgUrl { get; set; }
    public string? AvgResponseTime { get; set; }
    public string? FranchisedUnits { get; set; }
    public string? OwnedUnits { get; set; }
    public string? ProjectedNewUnits { get; set; }
    public string? YearEstablished { get; set; }
    public string? TypeOfBusiness { get; set; }
    public string? NumberOfEmployees { get; set; }
    public string? Territories { get; set; }
    public string? Liquidity { get; set; }
    public string? InvestmentRange { get; set; }
    public string? MinimumNetWorth { get; set; }
    public string? MonthCash { get; set; }
    public string? FranchiseFee { get; set; }
    public string? Royalty { get; set; }
    public string? RoyaltyDescription { get; set; }
    public string? Advertising { get; set; }
    public string? NationalAdvertising { get; set; }
    public string? RampUp { get; set; }
    public string? PassiveOwnership { get; set; }
    public string? Item19 { get; set; }
    public string? Single { get; set; }
    public string? Multiple { get; set; }
    public string? AreaRepresentativeMasterDeveloper { get; set; }
    public string? AvailableDiscount { get; set; }
    public string? Memberships { get; set; }

}
