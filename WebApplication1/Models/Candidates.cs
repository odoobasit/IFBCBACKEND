using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Candidates
    {
        [Key]
        public int DocId { get; set; }

        public DateTime? DocDate {  get; set; } = DateTime.Now; // Default to current date and time

        public DateTime? UpdateDate { get; set; }
        public string CloseDate { get; set; }
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
        public string DealSource { get; set; }
        public string DealSourceCost { get; set; }
        public string ZorackleValue { get; set; }
        public string DealValue { get; set; }
        public string About { get; set; }
        public string InvestmentFranchise { get; set; }
        public string Funding { get; set; }
        public string CreditScore { get; set; }
        public string InitialQualifyingNote { get; set; }
        public string Activities { get; set; }
        public string AttendingNetworkFunction { get; set; }
        public string MultiUnitOps { get; set; }
        public string BusinessPartner { get; set; }
        public string FamilyFeel { get; set; }
        public string EmployeesPrefer { get; set; }
        public string StaffSize { get; set; }
        public string ZorakleNotes { get; set; }
        public string FundingBusiness { get; set; }
        public string RetirementPlan { get; set; }
        public string VALoan { get; set; }
        public string CurrentNetworth { get; set; }
        public string TrafficViolation { get; set; }
        public string Unsatisfiedjudgment { get; set; }
        public string Bankruptcy { get; set; }
        public string EligibilityNote { get; set; }
        public string BusinessBefore { get; set; }
        public string MarketingExperience { get; set; }
        public string ManagementExperice { get; set; }
        public string SalesExperience { get; set; }
        public string ReviewFinancialStatement { get; set; }
        public string CSExperience { get; set; }
        public string AttractiveBusinessOwner { get; set; }
        public string HandleNewBusiness { get; set; }
        public string BusinessExpectations { get; set; }
        public string WantNote { get; set; }
        public string PreferB2b { get; set; }
        public string PhysicalLocation { get; set; }
        public string Inventory { get; set; }
        public string ColdCalling { get; set; }
        public string PassiveMode { get; set; }
        public string BusinessHours { get; set; }
        public string Networth { get; set; }
        public string LiquidCash { get; set; }
        public string Competency1 { get; set; }
        public string Competency2 { get; set; }
        public string Competency3 { get; set; }
        public string FranchiseCause { get; set; }
        public string ProfessionalBackground { get; set; }
        public string FranchiseInterested { get; set; }
        public string TimeFrame { get; set; }
        public string Status { get; set; }
        public string PipelineStep { get; set; }
        public string LostReason { get; set; }
        public string CategoryRating { get; set; }
        public int AgentUserId { get; set; }
        public bool isArchive { get; set; }
    }
}
