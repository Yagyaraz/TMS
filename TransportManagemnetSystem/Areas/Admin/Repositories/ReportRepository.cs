using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Security;
using TransportManagemnetSystem.Utilities;

namespace TransportManagemnetSystem.Areas.Admin.Repositories
{
    public class ReportRepository : IReport
    {
        private readonly IUtility _utility;
        private readonly TransportDbContext _context;
        private readonly ILogger<ReportRepository> _logger;
        private readonly string _userStateId;
        private readonly string _userId;
        private readonly string _userOrgId = null;
        private readonly IHttpContextAccessor _httpContext;
        public ReportRepository(TransportDbContext context, IHttpContextAccessor httpContext, ILogger<ReportRepository> logger, IUtility utility)
        {
            _context = context;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger = logger;
            _userStateId = httpContext.HttpContext.User.FindFirstValue("StateId");
            _userOrgId = httpContext.HttpContext.User.FindFirstValue("OrgId");
            _utility = utility;
        }

        public async Task<List<OrginizationReportViewModel>> GetAllOrganixzation()
        {
            //var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.OrganizationDetail.AsQueryable();

            //if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            //{
            //    query = query.Where(x => x.OrganizationDetail.StateId == int.Parse(_userStateId));
            //}
            if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => int.Parse(_userOrgId) == 0 || x.Id == int.Parse(_userOrgId));
            }
            return await query
                .Select(x => new OrginizationReportViewModel()
                {
                    Id = x.Id,
                    Area = x.Area,
                    DartaLocation = x.DartaLocation,
                    DartaMiti = x.DartaMiti,
                    DartaMitiEng = x.DartaMitiEng,
                    DistrictId = x.DistrictId,
                    Email = x.Email,
                    OrganizationName = x.OrganizationName,
                    OrganizationNameEng = x.OrganizationNameEng,
                    PalikaId = x.PalikaId,
                    PhoneNumber = x.PhoneNumber,
                    StateId = x.StateId,
                    WardNo = x.WardNo
                }).ToListAsync();
        }

        public async Task<OrginizationReportViewModel> GetAllOrganixzationById(int id)
        {
            return await _context.OrganizationDetail.Where(x => x.Role == UserRoles.SubAdmin && x.Id == id)
                .Select(x => new OrginizationReportViewModel()
                {
                    Id = x.Id,
                    Area = x.Area,
                    DartaLocation = x.DartaLocation,
                    DartaMiti = x.DartaMiti,
                    DartaMitiEng = x.DartaMitiEng,
                    DistrictId = x.DistrictId,
                    Email = x.Email,
                    OrganizationName = x.OrganizationName,
                    OrganizationNameEng = x.OrganizationNameEng,
                    PalikaId = x.PalikaId,
                    PhoneNumber = x.PhoneNumber,
                    StateId = x.StateId,
                    WardNo = x.WardNo,
                    StateName = _context.State.Where(z => z.Id == x.StateId).Select(z => z.StateNameNep).FirstOrDefault(),
                    DistrictName = _context.District.Where(z => z.Id == x.DistrictId).Select(z => z.DistrictNameNep).FirstOrDefault(),
                    PalikaName = _context.Palika.Where(z => z.Id == x.PalikaId).Select(z => z.PalikaNameNep).FirstOrDefault(),
                    MemberList = _context.Member.Where(z => z.OrganizationId == x.Id)
                    .Select(z => new MemberViewModel()
                    {
                        FullName = z.FullName,
                        MobileNo = z.MobileNo,
                        CitizenNo = z.CitizenNo,
                        PanVatNo = z.PanVatNo,
                    }).ToList() ?? new List<MemberViewModel>(),
                    VehicleList = _context.VehicleDarta.Where(z => z.OrganizationId == x.Id)
                    .Select(z => new VehicleDartaViewModel()
                    {
                        VehicleNumber = z.VehicleNumber,
                        EngineNo = z.EngineNo,
                        ChasisNo = z.ChasisNo,
                        VehicleTypeName = z.VehicleType.NameNep,
                    }).ToList() ?? new List<VehicleDartaViewModel>(),
                }).FirstOrDefaultAsync() ?? new OrginizationReportViewModel();
        }

        //public async Task<RouteSearchModel> RouteSearch(string source, string destination)
        //{
        //    var routes = await _context.BusRoute
        //.Where(r => r.SourceName == source && (
        //            r.DestinationName == destination ||
        //            _context.StopSetup.Any(s => s.RouteId == r.Id && s.Name == destination)))
        //.Select(r => new RouteViewModel
        //{
        //    Id = r.Id,
        //    RouteName = r.RouteName,
        //    SourceName = r.SourceName,
        //    DestinationName = r.DestinationName
        //})
        //.ToListAsync();

        //    var data = new RouteSearchModel
        //    {
        //        Routes = routes
        //    };

        //    return data;
        //}
        public async Task<RouteSearchModel> RouteSearch(string source, string destination)
        {
            var routes = await _context.BusRoute
                .Where(r =>
                    EF.Functions.Like(r.SourceName, $"%{source}%") &&
                    (
                        EF.Functions.Like(r.DestinationName, $"%{destination}%") ||
                        _context.StopSetup.Any(s => s.RouteId == r.Id && EF.Functions.Like(s.Name, $"%{destination}%"))
                    )
                )
                .Select(r => new RouteViewModel
                {
                    Id = r.Id,
                    RouteName = r.RouteName,
                    SourceName = r.SourceName,
                    DestinationName = r.DestinationName
                })
                .ToListAsync();

            var data = new RouteSearchModel
            {
                Routes = routes
            };

            return data;
        }

        public async Task<RouteViewModel> RouteDetailsById(int id)
        {
            return await _context.BusRoute.Where(x => x.Id == id)
                 .Select(x => new RouteViewModel
                 {
                     Id = x.Id,
                     DestinationLatitude = x.DestinationLatitude,
                     DestinationLongnitude = x.DestinationLongnitude,
                     DestinationName = x.DestinationName,
                     DestinationTime = x.DestinationTime,
                     RouteName = x.RouteName,
                     SourceLatitude = x.SourceLatitude,
                     SourceLongnitude = x.SourceLongnitude,
                     SourceName = x.SourceName,
                     SourceTime = x.SourceTime,
                     StopSetupViewModelList = _context.StopSetup.Where(z => z.RouteId == id)
                     .Select(z => new StopSetupViewModel()
                     {
                         Id = z.Id,
                         RouteId = z.RouteId,
                         Latitude = z.Latitude,
                         Longnitude = z.Longnitude,
                         Name = z.Name,
                         StopTime = z.StopTime,
                         Time = z.Time,
                     }).ToList() ?? new List<StopSetupViewModel>()
                 }).FirstOrDefaultAsync() ?? new RouteViewModel();
        }
        public async Task<RouteSearchModel> OutRouteSearch(string source, string destination)
        {
            var routes = await _context.BusOutRoute
                .Where(r =>
                    EF.Functions.Like(r.SourceName, $"%{source}%") &&
                    (
                        EF.Functions.Like(r.DestinationName, $"%{destination}%") ||
                        _context.OutStopSetup.Any(s => s.OutRouteId == r.Id && EF.Functions.Like(s.Name, $"%{destination}%"))
                    )
                )
                .Select(r => new RouteViewModel
                {
                    Id = r.Id,
                    RouteName = r.RouteName,
                    SourceName = r.SourceName,
                    DestinationName = r.DestinationName
                })
                .ToListAsync();

            var data = new RouteSearchModel
            {
                Routes = routes
            };

            return data;
        }

        public async Task<OutRouteViewModel> OutRouteDetailsById(int id)
        {
            return await _context.BusOutRoute.Where(x => x.Id == id)
                 .Select(x => new OutRouteViewModel
                 {
                     Id = x.Id,
                     DestinationLatitude = x.DestinationLatitude,
                     DestinationLongnitude = x.DestinationLongnitude,
                     DestinationName = x.DestinationName,
                     DestinationTime = x.DestinationTime,
                     RouteName = x.RouteName,
                     SourceLatitude = x.SourceLatitude,
                     SourceLongnitude = x.SourceLongnitude,
                     SourceName = x.SourceName,
                     SourceTime = x.SourceTime,
                     StopSetupViewModelList = _context.OutStopSetup.Where(z => z.OutRouteId == id)
                     .Select(z => new StopSetupViewModel()
                     {
                         Id = z.Id,
                         RouteId = z.OutRouteId,
                         Latitude = z.Latitude,
                         Longnitude = z.Longnitude,
                         Name = z.Name,
                         StopTime = z.StopTime,
                         Time = z.Time,
                     }).ToList() ?? new List<StopSetupViewModel>()
                 }).FirstOrDefaultAsync() ?? new OutRouteViewModel();
        }

        public async Task<List<VehicleDartaViewModel>> VehicleDartaList()
        {
            var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.VehicleDarta.AsQueryable();

            if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            {
                query = query.Where(x => x.OrganizationDetail.StateId == int.Parse(_userStateId));
            }
            else if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => orgid == 0 || x.OrganizationId == orgid);
            }

            return await query.Select(x => new VehicleDartaViewModel
            {
                Id = x.Id,
                EngineTypeId = x.EngineTypeId,
                BuildYear = x.BuildYear,
                ChasisNo = x.ChasisNo,
                EngineNo = x.EngineNo,
                ManufactureCompanyId = x.ManufactureCompanyId,
                StandardTypeId = x.StandardTypeId,
                TotalSeat = x.TotalSeat,
                VehicleCategoryId = x.VehicleCategoryId,
                VehicleModel = x.VehicleModel,
                VehicleNumber = x.VehicleNumber,
                VehicleTypeId = x.VehicleTypeId,
                Cilinder = x.Cilinder,
                HorsePower = x.HorsePower,
                VehicleColor = x.VehicleColor,
                VehicleMirrorColor = x.VehicleMirrorColor,
                SancharUpakaran = x.SancharUpakaran,
                TotalWeight = x.TotalWeight,
                KarPramanPatra = x.KarPramanPatra,
                Jachpass = x.Jachpass,
                PradushanPramanpatra = x.PradushanPramanpatra,
                RouteName = x.RouteName,
                OtherPraman = x.OtherPraman,
                DartaPramanPartaFile = x.DartaPramanPartaFile,
                ParichayePatraFile = x.ParichayePatraFile,
                LicenseFile = x.LicenseFile,
                ChasisLength = x.ChasisLength,
                IsApproved = x.IsApproved,
            }).ToListAsync();
        }
        public async Task<VehicleDartaReportViewModel> VehicleDartaDetailsById(int id)
        {
            return await _context.VehicleDarta.Where(x => x.Id == id)
                             .Select(x => new VehicleDartaReportViewModel()
                             {
                                 Id = x.Id,
                                 EngineTypeId = x.EngineTypeId,
                                 BuildYear = x.BuildYear,
                                 ChasisNo = x.ChasisNo,
                                 EngineNo = x.EngineNo,
                                 ManufactureCompanyId = x.ManufactureCompanyId,
                                 StandardTypeId = x.StandardTypeId,
                                 TotalSeat = x.TotalSeat,
                                 VehicleCategoryId = x.VehicleCategoryId,
                                 VehicleModel = x.VehicleModel,
                                 VehicleNumber = x.VehicleNumber,
                                 VehicleTypeId = x.VehicleTypeId,
                                 Cilinder = x.Cilinder,
                                 HorsePower = x.HorsePower,
                                 VehicleColor = x.VehicleColor,
                                 VehicleMirrorColor = x.VehicleMirrorColor,
                                 SancharUpakaran = x.SancharUpakaran,
                                 TotalWeight = x.TotalWeight,
                                 KarPramanPatra = x.KarPramanPatra,
                                 Jachpass = x.Jachpass,
                                 PradushanPramanpatra = x.PradushanPramanpatra,
                                 RouteName = x.RouteName,
                                 OtherPraman = x.OtherPraman,
                                 DartaPramanPartaFile = x.DartaPramanPartaFile,
                                 ParichayePatraFile = x.ParichayePatraFile,
                                 LicenseFile = x.LicenseFile,
                                 ChasisLength = x.ChasisLength,
                                 VehicleAssignedDataDetails = _context.VehicleAssignedData.Where(z => z.VehicleId == x.Id).Select(z => new VehicleAssignedDataViewModel()
                                 {
                                     Id = z.Id,
                                     VehicleId = z.VehicleId,
                                     ConductorId = z.ConductorId,
                                     DriverId = z.DriverId,
                                     RouteId = z.RouteId,

                                     VehicleName = _context.VehicleDarta.Where(y => y.Id == z.VehicleId).Select(y => y.VehicleNumber).FirstOrDefault(),
                                     ConductorName = _context.BusconductorDetails.Where(y => y.Id == z.ConductorId).Select(y => y.FullName).FirstOrDefault(),
                                     DriverName = _context.DriverDetails.Where(y => y.Id == z.DriverId).Select(y => y.FullName).FirstOrDefault(),
                                     RouteName = _context.BusRoute.Where(y => y.Id == z.RouteId).Select(y => y.RouteName).FirstOrDefault(),
                                 }).FirstOrDefault() ?? new VehicleAssignedDataViewModel()
                             }).FirstOrDefaultAsync() ?? new VehicleDartaReportViewModel();
        }

        public async Task<List<DriverDetailReportViewModel>> DriverList()
        {
            var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.DriverDetails.AsQueryable();

            //if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            //{
            //    query = query.Where(x => x.DriverDetails.StateId == int.Parse(_userStateId));
            //}
            if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => orgid == 0 || x.OrganizationId == orgid);
            }
            return await query
                  .Select(x => new DriverDetailReportViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      DistrictId = x.DistrictId,
                      PalikaId = x.PalikaId,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                      Relation = x.Relation,
                      Name = x.Name,
                      Contactno = x.Contactno,
                      DocPath = x.DocPath,
                      LicenseNo = x.LicenseNo,
                      DOE = x.DOE,
                      
                  }).ToListAsync();
        }
       public async Task<DriverDetailReportViewModel> DriverDetailReportById(int id)
        {
            var driver = await _context.DriverDetails.Where(x => x.Id == id)
                  .Select(x => new DriverDetailReportViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      StateName= x.State.StateNameNep,
                      DistrictId = x.DistrictId,
                      DistrictName = x.District.DistrictNameNep,
                      CitizenIssueDistrictName = x.District.DistrictNameNep,
                      PalikaId = x.PalikaId,
                      PalikaName = x.Palika.PalikaNameNep,
                      Gender = x.Gender.Name,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                      Relation = x.Relation,
                      Name = x.Name,
                      Contactno = x.Contactno,
                      DocPath = x.DocPath,
                      LicenseNo = x.LicenseNo,
                      DOE = x.DOE,
                      LisenceCatList = _context.DriverLicenseCategory.Where(z => z.DriverDetailsId == x.Id).Select(z => z.LicenseId).ToList(),                      
                  }).FirstOrDefaultAsync() ?? new DriverDetailReportViewModel();
            if (driver == null) return new DriverDetailReportViewModel();
            driver.DriverLicenseCategoryViewModelList = await _context.DriverLicenseCategory
        .Select(l => new DriverLicenseCategoryViewModel
        {
            LicenseId = l.Id,
            LicenseName = l.License.Name + "-" + l.License.CatName,
        }).ToListAsync();

            return driver;
        }
        public async Task<List<BusconductorDetailReportViewModel>> CoDriverList()
        {
            //var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.BusconductorDetails.AsQueryable();

            //if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            //{
            //    query = query.Where(x => x.BusconductorDetails.StateId == int.Parse(_userStateId));
            //}
            if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => int.Parse(_userOrgId) == 0 || x.OrganizationId == int.Parse(_userOrgId));
            }
            return await query
                  .Select(x => new BusconductorDetailReportViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      DistrictId = x.DistrictId,
                      PalikaId = x.PalikaId,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                      PPPhotoPath = x.PPPhotoPath,
                  }).ToListAsync();
        }
        public async Task<BusconductorDetailReportViewModel> CoDriverDetailReportById(int id)
        {
            return await _context.BusconductorDetails.Where(x => x.Id == id)
                  .Select(x => new BusconductorDetailReportViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      DistrictId = x.DistrictId,
                      PalikaId = x.PalikaId,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                      StateName =x.State.StateNameNep,
                      DistrictName = x.District.DistrictNameNep,
                      PalikaName = x.Palika.PalikaNameNep,
                      CitizenIssueDistrictName = x.District.DistrictNameNep,
                      Gender = x.Gender.Name,
                  }).FirstOrDefaultAsync() ?? new BusconductorDetailReportViewModel();
        }

    }
}

