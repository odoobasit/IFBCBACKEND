using System.ComponentModel.DataAnnotations;
using System.Xml;
using YourNamespace.Models;

namespace WebApplication1.Models
{
    public class franchisecategories
    {
        [Key]
        public int docid { get; set; }
        public Boolean isCompleted { get; set; }
        public int Advertising { get; set; }
        public int Automotive { get; set; }
        public int BeautySpa { get; set; }
        public int BusinessManagementCoaching { get; set; }
        public int BusinessServices { get; set; }
        public int ChildEducationSTEMTutoring { get; set; }
        public int ChildServicesProducts { get; set; }
        public int CleaningResidentialCommercial { get; set; }
        public int ComputerTechnology { get; set; }
        public int DistributionServices { get; set; }
        public int DryCleaningLaundry { get; set; }
        public int FinancialServices { get; set; }
        public int Fitness { get; set; }
        public int FoodBeverageRestaurantQSR { get; set; }
        public int FoodCoffeeTeaSmoothiesSweets { get; set; }
        public int FoodStoresCatering { get; set; }
        public int HealthMedical { get; set; }
        public int HealthWellness { get; set; }
        public int HomeImprovement { get; set; }
        public int InteriorExteriorDesign { get; set; }
        public int MaintenanceRepair { get; set; }
        public int MovingStorageJunkRemoval { get; set; }
        public int Painting { get; set; }
        public int PestControl { get; set; }
        public int PetCareGrooming { get; set; }
        public int PrintCopyMailing { get; set; }
        public int RealEstate { get; set; }
        public int Restoration { get; set; }
        public int Retail { get; set; }
        public int Security { get; set; }
        public int SeniorCareMedicalNonMedical { get; set; }
        public int Signs { get; set; }
        public int SpecialEventPlanning { get; set; }
        public int SportsRecreation { get; set; }
        public int Staffing { get; set; }
        public int TravelPlanning { get; set; }
        public int Vending { get; set; }
        public string Timezone { get; set; }
        public string PreferredCallTime { get; set; }


    }
}
