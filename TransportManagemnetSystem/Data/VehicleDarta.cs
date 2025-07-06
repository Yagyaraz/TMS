using Org.BouncyCastle.Asn1.Crmf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class VehicleDarta
    {
        [Key]
        public int Id { get; set; }
        public int VehicleCategoryId { get; set; }
        public int VehicleTypeId { get; set; }
        public int ManufactureCompanyId { get; set; }
        public int EngineTypeId { get; set; }
        public string VehicleNumber { get; set; }
        public string EngineNo { get; set; }
        public string ChasisNo { get; set; }
        public string ChasisLength { get; set; }
        public string BuildYear { get; set; }
        public int TotalSeat { get; set; }
        public string VehicleModel { get; set; }
        public int? StandardTypeId { get; set; }
        public int OrganizationId { get; set; }

        public int Cilinder { get; set; }
        public int HorsePower { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleMirrorColor { get; set; }
        public string SancharUpakaran { get; set; }
        public string TotalWeight { get; set; }
        public string KarPramanPatra { get; set; }
        public string Jachpass { get; set; }
        public string PradushanPramanpatra { get; set; }
        public string RouteName { get; set; }
        public string OtherPraman { get; set; }

        public string DartaPramanPartaFile { get; set; }
        public string ParichayePatraFile { get; set; }
        public string LicenseFile { get; set; }
        public bool? IsApproved { get; set; }
        public bool IsAssgned { get; set; }

        public string VehicleOwnerName { get; set; }
        public string MobileNo { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }


        [ForeignKey(nameof(VehicleCategoryId))]
        public VehicleCategory VehicleCategory { get; set; }
        [ForeignKey(nameof(VehicleTypeId))]
        public VehicleType VehicleType { get; set; }
        [ForeignKey(nameof(ManufactureCompanyId))]
        public ManufactureCompany ManufactureCompany { get; set; }
        [ForeignKey(nameof(EngineTypeId))]
        public EngineType EngineType { get; set; }
        [ForeignKey(nameof(StandardTypeId))]
        public StandardType StandardType { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public OrganizationDetail OrganizationDetail { get; set; }
    }
    public class VehicleDartaApprove
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Darja { get; set; }
        public string ApproveNo { get; set; }
        public int VehicheDatraId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(VehicheDatraId))]
        public VehicleDarta VehicleDarta { get; set; }
    }

    public class BookedSeat
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public bool IsBooked { get; set; }
        public int VehicleDartaId { get; set; }
        public string BookedBy { get; set; }
        public string BookedDate { get; set; }
        public DateTime? BookedDateEn { get; set; }

        [ForeignKey("VehicleDartaId")]
        public VehicleDarta VehicleDarta { get; set; }
    }
    public class VehicleAssignedData
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ConductorId { get; set; }
        public int RouteId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("VehicleId")]
        public VehicleDarta VehicleDarta { get; set; }
        [ForeignKey("DriverId")]
        public DriverDetails DriverDetails { get; set; }
        [ForeignKey("ConductorId")]
        public BusconductorDetails BusconductorDetails { get; set; }
        [ForeignKey("RouteId")]
        public BusRoute BusRoute { get; set; }
    }
    public class bluebookRouteBima
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string BluebookDartaMiti { get; set; }
        public DateTime? BluebookDartaDate { get; set; }
        public string BluebookEndMiti { get; set; }
        public DateTime? BluebookEndDate { get; set; }
        public string PollutionDartaMiti { get; set; }
        public DateTime? PollutionDartaDate { get; set; }
        public string PollutionEndMiti { get; set; }
        public DateTime? PollutionEndDate { get; set; }
        public string RouteDartaMiti { get; set; }
        public DateTime? RouteDartaDate { get; set; }
        public string RouteEndMiti { get; set; }
        public DateTime? RouteEndDate { get; set; }
        public string BimaCompanyName { get; set; }
        public string BimaDartaMiti { get; set; }
        public DateTime? BimaDartaDate { get; set; }
        public string BimaEndMiti { get; set; }
        public DateTime? BimaEndDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("VehicleId")]
        public VehicleDarta VehicleDarta { get; set; }
    }
}
