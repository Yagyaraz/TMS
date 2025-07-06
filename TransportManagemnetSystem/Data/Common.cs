using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class FiscalYear
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_En { get; set; }
        public string Code { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int DisplayPosition { get; set; }
        public bool IsActive { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public DateTime DateFromEng { get; set; }
        public DateTime DateToEng { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? PreviousFiscalYearId { get; set; }

        [ForeignKey("PreviousFiscalYearId")]
        public virtual FiscalYear PreviousFiscalYear { get; set; }
    }
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string StateName { get; set; }
        [Required]
        public string StateNameNep { get; set; }
        public string StateCode { get; set; }
    }
    public class District
    {
        [Key]
        public int Id { get; set; }
        public string DistrictName { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string DistrictNameNep { get; set; }
        public string DistrictCode { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
    public class Palika
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public string PalikaName { get; set; }
        [Required]
        public string PalikaNameNep { get; set; }
        public string PalikaCode { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
    }
    public class VehicleCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNep { get; set; }
    }
    public class VehicleType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNep { get; set; }
        public int VehicleCategoryId { get; set; }

        [ForeignKey(nameof(VehicleCategoryId))]
        public VehicleCategory VehicleCategory { get; set; }
    }
    public class ManufactureCompany
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNep { get; set; }
    }
    public class EngineType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNep { get; set; }
    }
    public class StandardType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNep { get; set; }
    }

    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_en { get; set; }
        public string Code { get; set; }
    }
    public class FAQ
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        [DataType(DataType.MultilineText)]
        public string Question { get; set; }

        [Required(ErrorMessage = "Answer is required.")]
        [DataType(DataType.MultilineText)]
        public string Answer { get; set; }
        public bool Status { get; set; } = true;
    }
}
