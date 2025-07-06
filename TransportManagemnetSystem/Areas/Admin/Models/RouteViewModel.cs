using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Areas.Admin.Models
{
    //public class RouteViewModel
    //{
    //    public int Id { get; set; }
    //    [Display(Name = "Route Name")]
    //    public string RouteName { get; set; }
    //    [Display(Name = "Source")]
    //    public string SourceName { get; set; }
    //    [Display(Name = "Source Time")]
    //    public TimeSpan SourceTime { get; set; }
    //    [Display(Name = "Source Latitude")]
    //    public string SourceLatitude { get; set; }
    //    [Display(Name = "Source Longnitude")]
    //    public string SourceLongnitude { get; set; }
    //    [Display(Name = "Destination Name")]
    //    public string DestinationName { get; set; }
    //    [Display(Name = "Destination Time")]
    //    public TimeSpan DestinationTime { get; set; }
    //    [Display(Name = "Destination Latitude")]
    //    public string DestinationLatitude { get; set; }
    //    [Display(Name = "Destination Longnitude")]
    //    public string DestinationLongnitude { get; set; }
    //    public List<StopSetupViewModel> StopSetupViewModelList { get; set; } = new List<StopSetupViewModel>();
    //}
    public class RouteViewModel
    {
        public int Id { get; set; }
        [Display(Name = "रुट नाम")]
        public string RouteName { get; set; }
        [Display(Name = "सुरु स्थान")]
        public string SourceName { get; set; }
        [Display(Name = "सुरु समय")]
        public TimeSpan SourceTime { get; set; }
        [Display(Name = "सुरु अक्षांश")]
        public string SourceLatitude { get; set; }
        [Display(Name = "सुरु देशान्तर")]
        public string SourceLongnitude { get; set; }
        [Display(Name = "गन्तव्य नाम")]
        public string DestinationName { get; set; }
        [Display(Name = "गन्तव्य समय")]
        public TimeSpan DestinationTime { get; set; }
        [Display(Name = "गन्तव्य अक्षांश")]
        public string DestinationLatitude { get; set; }
        [Display(Name = "गन्तव्य देशान्तर")]
        public string DestinationLongnitude { get; set; }
        public List<StopSetupViewModel> StopSetupViewModelList { get; set; } = new List<StopSetupViewModel>();
    }
    public class OutRouteViewModel
    {
        public int Id { get; set; }
        [Display(Name = "रुट नाम")]
        public string RouteName { get; set; }
        [Display(Name = "सुरु स्थान")]
        public string SourceName { get; set; }
        [Display(Name = "सुरु समय")]
        public TimeSpan SourceTime { get; set; }
        [Display(Name = "सुरु अक्षांश")]
        public string SourceLatitude { get; set; }
        [Display(Name = "सुरु देशान्तर")]
        public string SourceLongnitude { get; set; }
        [Display(Name = "गन्तव्य नाम")]
        public string DestinationName { get; set; }
        [Display(Name = "गन्तव्य समय")]
        public TimeSpan DestinationTime { get; set; }
        [Display(Name = "गन्तव्य अक्षांश")]
        public string DestinationLatitude { get; set; }
        [Display(Name = "गन्तव्य देशान्तर")]
        public string DestinationLongnitude { get; set; }
        public List<StopSetupViewModel> StopSetupViewModelList { get; set; } = new List<StopSetupViewModel>();
    }
    public class StopSetupViewModel
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan StopTime { get; set; }
        public string Latitude { get; set; }
        public string Longnitude { get; set; }
    }
    public class VehicleDartaReportViewModel
    {
        public int Id { get; set; }
        [Display(Name = "सवारी श्रेणी")]
        public int VehicleCategoryId { get; set; }
        [Display(Name = "सवारी प्रकार")]
        public int VehicleTypeId { get; set; }
        [Display(Name = "निर्माण कम्पनी")]
        public int ManufactureCompanyId { get; set; }
        [Display(Name = "इन्जिन प्रकार")]
        public int EngineTypeId { get; set; }
        [Display(Name = "गाडी नं")]
        public string VehicleNumber { get; set; }
        [Display(Name = "इन्जिन नम्बर")]
        public string EngineNo { get; set; }
        [Display(Name = "च्यासिस नम्बर")]
        public string ChasisNo { get; set; }
        [Display(Name = "चेसिसको लम्बाई")]
        public string ChasisLength { get; set; }
        [Display(Name = "निर्माण वर्ष")]
        public string BuildYear { get; set; }
        [Display(Name = "जम्मा सीट")]
        public int TotalSeat { get; set; }
        [Display(Name = "मोडेल")]
        public string VehicleModel { get; set; }
        [Display(Name = "गाडी श्रेणी")]
        public int? StandardTypeId { get; set; }

        [Display(Name = "सिलिन्डर")]
        public int Cilinder { get; set; }
        [Display(Name = "हर्ष पावर")]
        public int HorsePower { get; set; }
        [Display(Name = "सवारी बडी रङ्ग")]
        public string VehicleColor { get; set; }
        [Display(Name = "सवारी सिसा रङ्ग")]
        public string VehicleMirrorColor { get; set; }
        [Display(Name = "संचार उपकरण")]
        public string SancharUpakaran { get; set; }
        [Display(Name = "वजन उपकरण")]
        public string TotalWeight { get; set; }
        [Display(Name = "सवारी कर प्रमाणपत्र")]
        public string KarPramanPatra { get; set; }
        [Display(Name = "जांचपास")]
        public string Jachpass { get; set; }
        [Display(Name = "प्रदूषण प्रमाणपत्र")]
        public string PradushanPramanpatra { get; set; }
        [Display(Name = "सवारी संचालन गर्न रुट")]
        public string RouteName { get; set; }
        [Display(Name = "अन्य प्रमाण")]
        public string OtherPraman { get; set; }

        public string DartaPramanPartaFile { get; set; }
        public string ParichayePatraFile { get; set; }
        public string LicenseFile { get; set; }

        [Display(Name = "सवारी दर्ता प्रमाणपत्रको प्रतिलिपि")]
        public IFormFile IDartaPramanPartaFile { get; set; }
        [Display(Name = "सवारी धनीको प्रमाणपत्रको प्रतिलिपि")]
        public IFormFile IParichayePatraFile { get; set; }
        [Display(Name = "सवारी चालकको सवारी चालक प्रमाणपत्रको प्रतिलिपि")]
        public IFormFile ILicenseFile { get; set; }
        public bool? IsApproved { get; set; }

        public string VehicleTypeName { get; set; }

        public bool IsAssgned { get; set; }

        public VehicleAssignedDataViewModel VehicleAssignedDataDetails { get; set; } = new VehicleAssignedDataViewModel();
    }
    public class VehicleAssignedDataViewModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ConductorId { get; set; }
        public int RouteId { get; set; }

        [Display(Name = "गाडी नं")]
        public string VehicleName { get; set; }
        [Display(Name = "चालक को नाम")]
        public string DriverName { get; set; }
        [Display(Name = "सह-चालक को नाम")]
        public string ConductorName { get; set; }
        [Display(Name = "रुटको नाम")]
        public string RouteName { get; set; }


    }
}
