using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApplication1.Models;
using YourNamespace.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Listings> Listings { get; set; }

    public DbSet<ListingsView> ListingsView { get; set; }

    public DbSet<Candidates> Candidates {  get; set; }

    public DbSet<CandidateViewModel> CandidatesView { get; set; }

    public DbSet<Users> Users { get; set; }

    public DbSet<LoginRequest> LoginRequest { get; set; }

    public DbSet<Registrations> Registrations { get; set; }

    public DbSet<Checkout> Checkout { get; set; }

    public DbSet<Funding> Fundings { get; set; }

    public DbSet<ListingsContent> ListingsContent { get; set; }

    public DbSet<FundCalculator> FundCalculator { get; set; }

    public DbSet<ContactUs> ContactUs { get; set; }

    public DbSet<CandidateContacts> CandidateContacts { get; set; }

    public DbSet<TerritoryDetails> TerritoryDetails { get; set; }

    public DbSet<listingsMstr> listingsMstr { get; set; }

    public DbSet<listingsOwner> listingsOwner { get; set; }

    public DbSet<FranchiseIssue> FranchiseIssue { get; set; }

    public DbSet<Emailmstr> Emailmstr { get; set; }

    public DbSet<CandidatesMstr> CandidatesMstr { get; set; }

    public DbSet<CandidateProfile> CandidateProfile { get; set; }

    public DbSet<initialqualify> initialqualify { get; set; }

    public DbSet<eligibility> eligibility { get; set; }

    public DbSet<experience> experience { get; set; }

    public DbSet<wants> wants { get; set; }

    public DbSet<franchisecategories> franchisecategories { get; set; }
}
