using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Areas.Admin.Models
{
    public class DriverDetailViewModel
    {
        public int Id { get; set; }
        [Display(Name = "पुरा नाम(Eng)")]
        public string FullName { get; set; }
        [Display(Name = "पुरा नाम")]
        public string FullNameNep { get; set; }
        [Display(Name = "लिङ्ग")]
        public int? GenderId { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Mobile number")]
        [Display(Name = "मोबाइल नं")]
        public string MobileNo { get; set; }
        [Display(Name = "प्यान नं")]
        public int PanNo { get; set; }
        [Display(Name = "नागरिकता नं")]
        public string CitizenNo { get; set; }
        [Display(Name = "जारी जिल्ला")]
        public int? CitizenIssueDistrict { get; set; }
        [Display(Name = "जारी मिति")]
        public string JariMiti { get; set; }
        [Display(Name = "हजुरबुबा नाम")]
        public string GrandFatherName { get; set; }
        [Display(Name = "बाबुको नाम")]
        public string FatherName { get; set; }
        [Display(Name = "प्रदेश")]
        public int? StateId { get; set; }
        [Display(Name = "जिल्ला")]
        public int? DistrictId { get; set; }
        [Display(Name = "पालिका")]
        public int? PalikaId { get; set; }
        [Display(Name = "वार्ड")]
        public int? WardId { get; set; }
        public int OrganizationId { get; set; }
        [Display(Name = "हालको फोटो")]
        public IFormFile PPFilePath { get; set; }
        [Display(Name = "नागरिकताको फोटो")]
        public IFormFile CitizenFilePath { get; set; }

        public string PPPhotoPath { get; set; }
        public string CitizenPhotoPath { get; set; }
        [Display(Name = "नाता")]
        public string Relation { get; set; }
        [Display(Name = "पुरा नाम")]
        public string Name { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Mobile number")]
        [Display(Name = "मोबाइल नं")]
        public string Contactno { get; set; }
        public string DocPath { get; set; }

        [Display(Name = "फोटो")]
        public IFormFile RelationFilePath { get; set; }
        [Display(Name = "लाईसन नं")]
        public string LicenseNo { get; set; }
        [Display(Name = "लाईसन म्याद सकिने मिति")]
        public DateTime DOE { get; set; } = DateTime.Now;
        public IEnumerable<int> LisenceCatList { get; set; } = new List<int>();
        public List<DriverLicenseCategoryViewModel> DriverLicenseCategoryViewModelList { get; set; } = new List<DriverLicenseCategoryViewModel>();
    }
    public class DriverLicenseCategoryViewModel
    {
        public int Id { get; set; }
        public int DriverDetailsId { get; set; }
        public int LicenseId { get; set; }
        public string LicenseName { get; set; }
    }
    public class BusconductorDetailViewModel
    {
        public int Id { get; set; }
        [Display(Name = "पुरा नाम(Eng)")]
        public string FullName { get; set; }
        [Display(Name = "पुरा नाम")]
        public string FullNameNep { get; set; }
        [Display(Name = "लिङ्ग")]
        public int? GenderId { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Mobile number")]
        [Display(Name = "मोबाइल नं")]
        public string MobileNo { get; set; }
        [Display(Name = "प्यान नं")]
        public int PanNo { get; set; }
        [Display(Name = "नागरिकता नं")]
        public string CitizenNo { get; set; }
        [Display(Name = "जारी जिल्ला")]
        public int? CitizenIssueDistrict { get; set; }
        [Display(Name = "जारी मिति")]
        public string JariMiti { get; set; }
        [Display(Name = "हजुरबुबा नाम")]
        public string GrandFatherName { get; set; }
        [Display(Name = "बाबुको नाम")]
        public string FatherName { get; set; }
        [Display(Name = "प्रदेश")]
        public int? StateId { get; set; }
        [Display(Name = "जिल्ला")]
        public int? DistrictId { get; set; }
        [Display(Name = "पालिका")]
        public int? PalikaId { get; set; }
        [Display(Name = "वार्ड")]
        public int? WardId { get; set; }
        public int OrganizationId { get; set; }
        [Display(Name = "हालको फोटो")]
        public IFormFile PPFilePath { get; set; }
        [Display(Name = "नागरिकताको फोटो")]
        public IFormFile CitizenFilePath { get; set; }

        public string PPPhotoPath { get; set; }
        public string CitizenPhotoPath { get; set; }
    }
}
