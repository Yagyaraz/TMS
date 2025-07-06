using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Areas.Admin.Models
{

    public class FiscalYearViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "नाम")]
        public string Name { get; set; }
        [Display(Name = "नाम(English)")]
        public string Name_En { get; set; }
        [Required]
        [Display(Name = "कोड")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "सुरु वर्ष")]
        public int StartYear { get; set; }
        [Required]
        [Display(Name = "अन्तिम वर्ष")]
        public int EndYear { get; set; }
        [Display(Name = "प्रदर्शन स्थिति")]
        public int DisplayPosition { get; set; }
        [Display(Name = "स्थिति")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "सुरु मिति")]
        public string DateFrom { get; set; }
        [Required]
        [Display(Name = "अन्तिम मिति")]
        public string DateTo { get; set; }
        [Display(Name = "सुरु मिति(English)")]
        [DataType(DataType.Date)]
        public DateTime DateFromEng { get; set; } = DateTime.Now;
        [Display(Name = "अन्तिम मिति(English)")]
        [DataType(DataType.Date)]
        public DateTime DateToEng { get; set; } = DateTime.Now;
        [Display(Name = "अघिल्लो वर्ष")]
        public int? PreviousFiscalYearId { get; set; }
        [Display(Name = "अघिल्लो वर्ष")]
        public string PreviousFiscalYearName { get; set; }
    }

    public class OfficeViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "कार्यालय नाम")]
        public string Name { get; set; } = "TMS";
        [Required]
        [Display(Name = "कोड")]
        public string Code { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "फोन नं")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string PhoneNo { get; set; }
        [Display(Name = "फ्याक्स नं")]
        public string FaxNo { get; set; }
        [Display(Name = "इमेल")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "कार्यालय आधिकारिक वेबसाइट")]
        public string Url { get; set; }
        [Display(Name = "प्रदेश")]
        public int? StateId { get; set; }
        [Display(Name = "जिल्ला")]
        public int? DistrictId { get; set; }
        [Display(Name = "नगरपालिका")]
        public int? PalikaId { get; set; }

        [Display(Name = "प्रदेश")]
        public string StateName { get; set; }
        [Display(Name = "जिल्ला")]
        public string DistrictName { get; set; }
        [Display(Name = "नगरपालिका")]
        public string PalikaName { get; set; }
    }

    public class VehicleCategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "नाम(English)")]
        public string Name { get; set; }
        [Display(Name = "नाम")]
        public string NameNep { get; set; }
    }

    public class VehicleTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "नाम(English)")]
        public string Name { get; set; }
        [Display(Name = "नाम")]
        public string NameNep { get; set; }
        [Display(Name = "सवारी प्रकार")]
        public int VehicleCategoryId { get; set; }
    }

    public class ManufactureCompanyViewModel
    {
        public int Id { get; set; }
        [Display(Name = "नाम(English)")]
        public string Name { get; set; }
        [Display(Name = "नाम")]
        public string NameNep { get; set; }
    }

    public class StandardTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "नाम(English)")]
        public string Name { get; set; }
        [Display(Name = "नाम")]
        public string NameNep { get; set; }
    }
    public class EngineTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "नाम(English)")]
        public string Name { get; set; }
        [Display(Name = "नाम")]
        public string NameNep { get; set; }
    }
    public class FAQViewModel
    {
        public int Id { get; set; }
        [Display(Name ="प्रश्न")]
        public string Question { get; set; } = null!;
        [Display(Name ="उत्तर")]
        public string Answer { get; set; }
        public bool Status { get; set; } = true;
    }
}
