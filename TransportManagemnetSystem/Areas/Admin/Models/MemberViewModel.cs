using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Areas.Admin.Models
{
    //public class MemberViewModel
    //{
    //    public int Id { get; set; }
    //    [Display(Name = "Comittee Name")]
    //    public int OrganizationId { get; set; }
    //    [Display(Name = "Full Name(नेपाली)")]
    //    public string FullName { get; set; }
    //    [Display(Name = "Full Name")]
    //    public string FullNameEng { get; set; }
    //    public string CitizenNo { get; set; }
    //    [Display(Name = "Issue Date")]
    //    public string CitizenIssueDate { get; set; }
    //    [Display(Name = "Issue District")]
    //    public int? CitizenIssueDistricrId { get; set; }
    //    public string RegNo { get; set; }
    //    [Display(Name = "Pan/Vat No")]
    //    public string PanVatNo { get; set; }
    //    public string Address { get; set; }
    //    public string Email { get; set; }
    //    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Mobile number")]
    //    public string MobileNo { get; set; }
    //    public string FaxNo { get; set; }
    //    [Display(Name = "Current Photo")]
    //    public IFormFile PPFilePath { get; set; }
    //    [Display(Name = "Citizen Photo")]
    //    public IFormFile CitizenFilePath { get; set; }

    //    public string PPPhotoPath { get; set; }
    //    public string CitizenPhotoPath { get; set; }


    //}

    public class MemberViewModel
    {
        public int Id { get; set; }
        [Display(Name = "संस्थाको नाम")]
        public int OrganizationId { get; set; }
        [Display(Name = "पुरा नाम")]
        public string FullName { get; set; }
        [Display(Name = "पुरा नाम(Eng)")]
        public string FullNameEng { get; set; }
        [Display(Name = "नागरिकताको नं")]
        public string CitizenNo { get; set; }
        [Display(Name = "नागरिकता जारी मिति")]
        public string CitizenIssueDate { get; set; }
        [Display(Name = "नागरिकता जारी जिल्ला")]
        public int? CitizenIssueDistricrId { get; set; }
        [Display(Name = "दर्ता नं")]
        public string RegNo { get; set; }
        [Display(Name = "प्यान/भ्याट नं")]
        public string PanVatNo { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "इमेल")]
        public string Email { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Mobile number")]
        [Display(Name = "मोबाइल नं")]
        public string MobileNo { get; set; }
        [Display(Name = "फ्याक्स नं")]
        public string FaxNo { get; set; }
        [Display(Name = "आफ्नो हालसाल फोटो")]
        public IFormFile PPFilePath { get; set; }
        [Display(Name = "नागरिकताको फोटो")]
        public IFormFile CitizenFilePath { get; set; }

        public string PPPhotoPath { get; set; }
        public string CitizenPhotoPath { get; set; }


    }
}
