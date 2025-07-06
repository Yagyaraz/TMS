using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Areas.Admin.Models
{
    //public class VehicleDartaViewModel
    //{
    //    public int Id { get; set; }
    //    [Display(Name = "Vehicle Category")]
    //    public int VehicleCategoryId { get; set; }
    //    [Display(Name = "Vehicle Type")]
    //    public int VehicleTypeId { get; set; }
    //    [Display(Name = "Manufacture Company")]
    //    public int ManufactureCompanyId { get; set; }
    //    [Display(Name = "Engine Type")]
    //    public int EngineTypeId { get; set; }
    //    [Display(Name = "Vehicle Number")]
    //    public string VehicleNumber { get; set; }
    //    [Display(Name = "EngineNo")]
    //    public string EngineNo { get; set; }
    //    [Display(Name = "ChasisNo")]
    //    public string ChasisNo { get; set; }
    //    [Display(Name = "ChasisLength")]
    //    public string ChasisLength { get; set; }
    //    [Display(Name = "Build Year")]
    //    public string BuildYear { get; set; }
    //    [Display(Name = "Total Seat")]
    //    public int TotalSeat { get; set; }
    //    [Display(Name = "Vehicle Model")]
    //    public string VehicleModel { get; set; }
    //    [Display(Name = "Standard Type")]
    //    public int? StandardTypeId { get; set; }

    //    public int Cilinder { get; set; }
    //    public int HorsePower { get; set; }
    //    [Display(Name = "Vehicle Color")]
    //    public string VehicleColor { get; set; }
    //    [Display(Name = "Vehicle Mirror Color")]
    //    public string VehicleMirrorColor { get; set; }
    //    [Display(Name = "Sanchar Upakaran")]
    //    public string SancharUpakaran { get; set; }
    //    [Display(Name = "Total Weight")]
    //    public string TotalWeight { get; set; }
    //    [Display(Name = "Kar PramanPatra No")]
    //    public string KarPramanPatra { get; set; }
    //    public string Jachpass { get; set; }
    //    [Display(Name = "Pollution Certificate")]
    //    public string PradushanPramanpatra { get; set; }
    //    [Display(Name = "Route Name")]
    //    public string RouteName { get; set; }
    //    [Display(Name = "Other Praman")]
    //    public string OtherPraman { get; set; }

    //    public string DartaPramanPartaFile { get; set; }
    //    public string ParichayePatraFile { get; set; }
    //    public string LicenseFile { get; set; }

    //    [Display(Name = "सवारी दर्ता प्रमाणपत्रको प्रतिलिपि")]
    //    public IFormFile IDartaPramanPartaFile { get; set; }
    //    [Display(Name = "सवारी धनीको प्रमाणपत्रको प्रतिलिपि")]
    //    public IFormFile IParichayePatraFile { get; set; }
    //    [Display(Name = "सवारी चालकको सवारी चालक प्रमाणपत्रको प्रतिलिपि")]
    //    public IFormFile ILicenseFile { get; set; }
    //    public bool? IsApproved { get; set; }

    //    public string VehicleTypeName { get; set; }
    //}

    //public class VehicleDartaAddImageViewModel
    //{
    //    public int Id { get; set; }
    //    public int VehicleDartaId { get; set; }
    //    [Display(Name = "Files")]
    //    public string FilePath { get; set; }
    //    public string FileName { get; set; }
    //    public IFormFile Files { get; set; }
    //    public List<VehicleDartaAddImageViewModel> FileList { get; set; } = new List<VehicleDartaAddImageViewModel>();
    //    //public List<ImageViewModel> ImageViewModelList { get; set; } = new List<ImageViewModel>();
    //}
    public class VehicleDartaViewModel
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

        [Display(Name = "सवारी धनिको नाम")]
        public string VehicleOwnerName { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Mobile number")]
        [Display(Name = "मोबाइल नं")]
        public string MobileNo { get; set; }

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
        public bluebookRouteBimaViewModel bluebookRouteBimaViewModelList { get; set; } = new bluebookRouteBimaViewModel();
    }
    public class bluebookRouteBimaViewModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [Display(Name = "Bluebook दर्ता मिति")]
        public string BluebookDartaMiti { get; set; }
        public DateTime? BluebookDartaDate { get; set; }
        [Display(Name = "Bluebook समाप्त मिति")]
        public string BluebookEndMiti { get; set; }
        public DateTime? BluebookEndDate { get; set; }
        [Display(Name = "प्रदूषण दर्ता मिति")]
        public string PollutionDartaMiti { get; set; }
        public DateTime? PollutionDartaDate { get; set; }
        [Display(Name = "प्रदूषण समाप्त मिति")]
        public string PollutionEndMiti { get; set; }
        public DateTime? PollutionEndDate { get; set; }
        [Display(Name = "रुट जारी मिति")]
        public string RouteDartaMiti { get; set; }
        public DateTime? RouteDartaDate { get; set; }
        [Display(Name = "रुट समाप्त मिति")]
        public string RouteEndMiti { get; set; }
        public DateTime? RouteEndDate { get; set; }
        [Display(Name = "बीमा कम्पनीको नाम")]
        public string BimaCompanyName { get; set; }
        [Display(Name = "बीमा जारी मिति")]
        public string BimaDartaMiti { get; set; }
        public DateTime? BimaDartaDate { get; set; }
        [Display(Name = "बीमा समाप्त मिति")]
        public string BimaEndMiti { get; set; }
        public DateTime? BimaEndDate { get; set; }
    }
    public class VehicleDartaAddImageViewModel
    {
        public int Id { get; set; }
        public int VehicleDartaId { get; set; }
        [Display(Name = "कागजातहरु")]
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public IFormFile Files { get; set; }
        public List<VehicleDartaAddImageViewModel> FileList { get; set; } = new List<VehicleDartaAddImageViewModel>();
        //public List<ImageViewModel> ImageViewModelList { get; set; } = new List<ImageViewModel>();
    }


    //for archival model
    public class VehicleDartaFilesViewModel
    {
        [Display(Name = "गाडी नं")]
        public int VehicleDartaId { get; set; }
        public string VehicleNumber { get; set; }
        [Display(Name = "सवारी दर्ता प्रमाणपत्रको प्रतिलिपि")]
        public string DartaPramanPartaFile { get; set; }
        [Display(Name = "सवारी धनीको प्रमाणपत्रको प्रतिलिपि")]
        public string ParichayePatraFile { get; set; }
        [Display(Name = "सवारी चालकको सवारी चालक प्रमाणपत्रको प्रतिलिपि")]
        public string LicenseFile { get; set; }
        public List<VehicleDartaAddImageViewModel> AdditionalFiles { get; set; } = new List<VehicleDartaAddImageViewModel>();
    }
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }
    }
    public class BookingRequest
    {
        public int Vehicleid { get; set; }
        public List<int> SeatNumbers { get; set; }
    }
   

    public class MyBookedSeatViewModel
    {
        public string VehicleNumb { get; set; }
        public int SeatNo { get; set; }
    }
}
