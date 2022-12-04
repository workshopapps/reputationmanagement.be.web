using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace src.Entities
{
    public class CareerResponse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]  
        public string PhoneNumber { get; set; }
        [Required]
        public LawyerType Position { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string ResumeFileName { get; set; }
        [Required]
        public string CoverLetterFileName { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }

    public enum LawyerType
    {
        [Description("Bankruptcy Lawyer")]
        BankruptcyLawyer,
        [Description("Business Lawyer (Corporate Lawyer)")]
        BusinessLawyer,
        [Description("Constitutional Lawyer")]
        ConstitutionalLawyer,
        [Description("Criminal Defense Lawyer")]
        CriminalDefenceLawyer,
        [Description("Employement and Labour Lawyer")]
        EmployementLaborLawyer,
        [Description("Employement and Labour Lawyer")]
        EntertainmentLawyer,
        [Description("Entertainment Lawyer")]
        EstatePlanningLawyer,
        [Description("Estate Planning Lawyer")]
        FamilyLawyer,
        [Description("Family Lawyer")]
        ImmigrationLawyer,
        [Description("Intellectual Property (IP) Lawyer")]
        IntellectualPropertyLawyer,
        [Description("Peronal Injury lawyer")]
        PersonalInjuryLawyer,
        [Description("Tax Lawyer")]
        TaxLawyer
    }
}
