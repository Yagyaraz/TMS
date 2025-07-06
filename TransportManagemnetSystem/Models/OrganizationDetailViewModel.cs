using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Models
{
    public class OrganizationDetailViewModel
    {
        public int Id { get; set; }
        [Display(Name = "समितिको नाम")]
        public string OrganizationName { get; set; }
        [Display(Name = "समितिको नाम(eng)")]
        public string OrganizationNameEng { get; set; }
        [Display(Name = "प्रदेश")]
        public int StateId { get; set; }
        [Display(Name = "जिल्ला")]
        public int DistrictId { get; set; }
        [Display(Name = "पालिका")]
        public int PalikaId { get; set; }
        [Display(Name = "वडा")]
        public int WardNo { get; set; }
        [Display(Name = "टोल")]
        public string Area { get; set; }
        [Display(Name = "इमेल")]
        public string Email { get; set; }
        [Display(Name = "दर्ता मिति")]
        public string DartaMiti { get; set; }
        public DateTime? DartaMitiEng { get; set; }
        [Display(Name = "दर्ता स्थान")]
        public string DartaLocation { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }

        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string PalikaName { get; set; }
    }
    public class OuterUserRegistrationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "पुरा नाम")]
        public string FullName { get; set; }
        [Display(Name = "पुरा नाम(Eng)")]
        public string FullNameEng { get; set; }
        [Display(Name = "नागरिकता नं")]
        public string CitizenNo { get; set; }
        [Display(Name = "नागरिकता जारी मिति")]
        public string CitizenIssueMiti { get; set; }
        public DateTime? CitizenIssueMitiEng { get; set; }
        [Display(Name = "प्रदेश")]
        public int StateId { get; set; }
        [Display(Name = "जिल्ला")]
        public int DistrictId { get; set; }
        [Display(Name = "पालिका")]
        public int PalikaId { get; set; }
        [Display(Name = "वडा")]
        public int WardNo { get; set; }
        [Display(Name = "टोल")]
        public string Area { get; set; }
        [Display(Name = "इमेल")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        [Display(Name = "संस्थाको नाम")]
        public int OrganizationId { get; set; }

    }
}
