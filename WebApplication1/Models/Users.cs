using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Users
    {
        [Key]
        public int DocId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string MeetingLink { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string ZipPostalCode { get; set; }
        public string UnitSuite { get; set; }
        public string Notes { get; set; }
        public string ShortDescription { get; set; }
        public string Consulting { get; set; }
        public string FranchiseIndustryFocus { get; set; }
        public Boolean BusinessBroker { get; set; }
        public string RegisteredIn { get; set; }
        public Boolean OpenForGroup { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Boolean TerritoryCheck { get; set; }
        public Boolean DisableLogo { get; set; }
        public Boolean DisableCover { get; set; }
        public Boolean DisableProfile { get; set; }
        public Boolean DisableBio { get; set; }
        public Boolean HideName { get; set; }
        public string Broker { get; set; }
        public Boolean AllCandidates { get; set; }
        public Boolean AllPastClient { get; set; }
        public Boolean ShareFranchise { get; set; }
        public string LeadEmail { get; set; }
        public Boolean FbaBadges { get; set; }
        public string UserType { get; set; } = "N";
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public DateTime DocDate { get; set; } = DateTime.Now;
    }
}
