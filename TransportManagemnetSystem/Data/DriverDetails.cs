using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class DriverDetails
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FullNameNep { get; set; }
        public int? GenderId { get; set; }
        public string MobileNo { get; set; }
        public int PanNo { get; set; }
        public string CitizenNo { get; set; }
        public int? CitizenIssueDistrict { get; set; }
        public string JariMiti { get; set; }
        public string GrandFatherName { get; set; }
        public string FatherName { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? PalikaId { get; set; }
        public int? WardId { get; set; }
        public string PPPhotoPath { get; set; }
        public string CitizenPhotoPath { get; set; }
        public int OrganizationId { get; set; }

        public string LicenseNo { get; set; }
        public DateTime DOE { get; set; }
        public string Relation { get; set; }
        public string Name { get; set; }
        public string Contactno { get; set; }
        public string DocPath { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }
        [ForeignKey("PalikaId")]
        public Palika Palika { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        [ForeignKey("OrganizationId")]
        public OrganizationDetail OrganizationDetail { get; set; }
    }
    public class DriverLicenseCategory
    {
        [Key]
        public int Id { get; set; }
        public int DriverDetailsId { get; set; }
        public int LicenseId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [ForeignKey(nameof(DriverDetailsId))]
        public DriverDetails DriverDetails { get; set; }
        [ForeignKey(nameof(LicenseId))]
        public DrivingLicense License { get; set; }
    }
    public class DrivingLicense
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatName { get; set; }

    }
    public class BusconductorDetails
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FullNameNep { get; set; }
        public int? GenderId { get; set; }
        public string MobileNo { get; set; }
        public int PanNo { get; set; }
        public string CitizenNo { get; set; }
        public int? CitizenIssueDistrict { get; set; }
        public string JariMiti { get; set; }
        public string GrandFatherName { get; set; }
        public string FatherName { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? PalikaId { get; set; }
        public int? WardId { get; set; }
        public string PPPhotoPath { get; set; }
        public string CitizenPhotoPath { get; set; }
        public int OrganizationId { get; set; }


        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }
        [ForeignKey("PalikaId")]
        public Palika Palika { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        [ForeignKey("OrganizationId")]
        public OrganizationDetail OrganizationDetail { get; set; }
    }
}
