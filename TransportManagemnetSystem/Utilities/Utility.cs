using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Models;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Utilities
{
    public class Utility : IUtility
    {
        private readonly TransportDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<Utility> _logger;
        private readonly string _userId;
        private readonly string _userOrgId;
        private readonly ConfigureSmsModel _smsModel;


        public Utility(TransportDbContext context, IWebHostEnvironment webHost, IHttpContextAccessor httpContext, IHttpContextAccessor httpContextAccessor, ILogger<Utility> logger, IOptions<ConfigureSmsModel> smsModel)
        {
            _context = context;
            _webHostEnvironment = webHost;
            _httpContext = httpContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userOrgId = httpContext.HttpContext.User.FindFirstValue("OrgId");
            _smsModel = smsModel.Value;


        }
        public async Task<SelectList> GetAllFiscalYearList()
        {
            return new SelectList(await _context.FiscalYear.OrderByDescending(x => x.StartYear).ToListAsync(), "Id", "Name");
        }
        public async Task<string> GetProjectName()
        {
            string name = await _context.Office.Select(x => x.Name).FirstOrDefaultAsync() ?? "TMS";
            return name;
        }
        public async Task<SelectList> GetSelectListRoles()
        {
            return new SelectList(await _context.Roles.Where(x => x.Name != UserRoles.Administrator && x.Name != UserRoles.SuperAdmin).ToListAsync(), "Name", "Name");
        }
        public async Task<SelectList> GetUserList()
        {
            return new SelectList(await _context.Users.Where(x => x.FullName != "Administrator").ToListAsync(), "Id", "UserName");
        }
        public async Task<SelectList> GetOrganizationSelectListItems()
        {
            return new SelectList(await _context.OrganizationDetail.Where(x => x.Role == UserRoles.SubAdmin).ToListAsync(), "Id", "OrganizationName");
        }
        public async Task<SelectList> GetAllVehicleCategoryList()
        {
            return new SelectList(await _context.VehicleCategory.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetAllVehicleTypeList(int? vehiclecatid)
        {
            return new SelectList(await _context.VehicleType.Where(x => (vehiclecatid == null || x.VehicleCategoryId == vehiclecatid)).ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetAllStandardTypeList()
        {
            return new SelectList(await _context.StandardType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetAllEngineTypeList()
        {
            return new SelectList(await _context.EngineType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetAllManufactureCompanyList()
        {
            return new SelectList(await _context.ManufactureCompany.ToListAsync(), "Id", "Name");
        }
        ///state
        public async Task<SelectList> GetStateSelectListItems()
        {
            return new SelectList(await _context.State.ToListAsync(), "Id", "StateNameNep");
        }

        public async Task<SelectList> GetDistrictSelectListItems(int? stateId)
        {
            return new SelectList(await _context.District.Where(x => (stateId == null || x.StateId == stateId)).ToListAsync(), "Id", "DistrictNameNep");
        }
        public async Task<SelectList> GetPalikaSelectListItems(int? distId)
        {
            return new SelectList(await _context.Palika.Where(x => (distId == null || x.DistrictId == distId)).ToListAsync(), "Id", "PalikaNameNep");
        }


        public async Task<SelectList> GetGenderSelectListItems()
        {
            return new SelectList(await _context.Gender.ToListAsync(), "Id", "Name");
        }

        public async Task<SelectList> GetLicenseSelectListItems()
        {
            var data = await _context.DrivingLicense.Select(x => new CommonModel()
            {
                Id = x.Id,
                Name = x.Name + "-" + x.CatName,
            }).ToListAsync();
            return new SelectList(data, "Id", "Name");
        }
        public async Task<int> GetLoggedInOrgId()
        {
            var data = await _context.OrganizationDetail.Where(x => x.UserId == _userId && x.Role == UserRoles.SubAdmin).Select(x => x.Id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<FileUploadModel> UploadImgReturnPathAndName(string folderName, IFormFile file, string name)
        {
            try
            {
                var model = new FileUploadModel();
                string defaultFolder = "UploadAllFiles";
                if (file == null) return model;
                name = string.IsNullOrEmpty(name) ? "TransportManagementSystem" : name;
                var fileExt = Path.GetExtension(file.FileName).Substring(1);
                folderName = string.IsNullOrEmpty(folderName) ? defaultFolder : folderName;
                folderName = folderName.Equals(defaultFolder) ? $"{defaultFolder}/AppImage/" : $"{defaultFolder}/{folderName}/";
                model.FileName = name + "_" + Guid.NewGuid().ToString() + "." + fileExt;
                var returnPath = folderName + model.FileName;
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    _webHostEnvironment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);// if Path not present than create

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, returnPath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.FilePath = "/" + returnPath;
                return model;
            }
            catch (Exception ex)
            {
                return new FileUploadModel();
            }
        }
        public Task RemoveFileFormServer(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) return Task.CompletedTask;
                string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot" + path);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("File Remove Error : " + ex.Message);
            }
            return Task.CompletedTask;
        }

        public async Task<List<PiechartViewModel>> GetPieChartDataOrg()
        {
            var org = await _context.OrganizationDetail.Where(x => x.Role == UserRoles.SubAdmin).Select(x => new PiechartViewModel()
            {
                Id = x.Id,
                Name = x.OrganizationName,
            }).ToListAsync();

            foreach (var item in org)
            {
                item.Count = await _context.Member.Where(x => x.OrganizationId == item.Id).CountAsync();
            }
            return org;
        }
        public async Task<List<PiechartAppDispViewModel>> GetPieChartDataApprovUnApprov()
        {
            var result = new List<PiechartAppDispViewModel>();
            var approvedCount = await _context.VehicleDarta
       .Where(x => x.IsApproved == true)
       .CountAsync();
            var unapprovedCount = await _context.VehicleDarta
       .Where(x => x.IsApproved != true)
       .CountAsync();


            result.Add(new PiechartAppDispViewModel()
            {
                Name = "Approved",
                Count = approvedCount
            });
            result.Add(new PiechartAppDispViewModel()
            {
                Name = "Disapproved/Pending",
                Count = unapprovedCount
            });
            return result;
        }
        public async Task<List<ColumnChartViewModel>> GetColumnChart()
        {
            var committeelist = await _context.OrganizationDetail.Where(x => x.Role == UserRoles.SubAdmin)
                .Select(x => new ColumnChartViewModel()
                {
                    Id = x.UserId,
                    Name = x.OrganizationName,
                }).ToListAsync();
            foreach (var item in committeelist)
            {
                item.count = await _context.VehicleDarta.Where(x => x.CreatedBy == item.Id).CountAsync();
            }
            return committeelist.OrderByDescending(x => x.count).Take(7).ToList();
        }
        public async Task<List<PieChartForRolesViewModel>> GetPieChartForRoles()
        {
            var roleNames = new List<string>
    {
        UserRoles.Admin,
        UserRoles.SubAdmin,
        UserRoles.User,
    };

            var result = new List<PieChartForRolesViewModel>();
            for (int i = 0; i < roleNames.Count; i++)
            {
                result.Add(new PieChartForRolesViewModel
                {
                    Name = roleNames[i],
                });
            }
            foreach (var item in result)
            {
                if (item.Name == UserRoles.User)
                {
                    item.count = await _context.OuterUserRegistration.CountAsync();
                }
                else
                {
                    item.count = await _context.OrganizationDetail
                        .Where(x => x.Role == item.Name)
                        .CountAsync();
                }
            }

            return result;
        }
        public async Task<List<LineChartViewModel>> GetVehicleChartData()
        {
            var result = await _context.VehicleType.Select(x => new LineChartViewModel()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToListAsync();
            foreach (var item in result)
            {
                item.count = await _context.VehicleDarta.Where(x => x.VehicleTypeId == item.Id).CountAsync();
            }
            return result;
        }

        public async Task<List<PiechartViewModel>> GetRouteLineChart()
        {
            var org = await _context.BusRoute.Select(x => new PiechartViewModel()
            {
                Id = x.Id,
                Name = x.RouteName,
            }).ToListAsync();

            foreach (var item in org)
            {
                item.Count = await _context.StopSetup.Where(x => x.RouteId == item.Id).CountAsync();
            }
            return org;
        }

        public async Task<SelectList> GetAllVehicleList()
        {
            return new SelectList(await _context.VehicleDarta.Where(x => x.CreatedBy == _userId).ToListAsync(), "Id", "VehicleNumber");
        }
        public async Task<SelectList> GetAllDriverList()
        {
            return new SelectList(await _context.DriverDetails.Where(x => x.CreatedBy == _userId).ToListAsync(), "Id", "FullName");
        }
        public async Task<SelectList> GetAllConductorList()
        {
            return new SelectList(await _context.BusconductorDetails.Where(x => x.CreatedBy == _userId).ToListAsync(), "Id", "FullName");
        }
        public async Task<SelectList> GetAllRouteList()
        {
            return new SelectList(await _context.BusRoute.Where(x => x.CreatedBy == _userId).ToListAsync(), "Id", "RouteName");
        }

        /// <summary>
        /// org data
        /// </summary>
        /// <returns></returns>

        public async Task<List<PiechartViewModel>> GetPieDataNumberOne()
        {
            var result = new List<PiechartViewModel>();

            // Total Drivers
            result.Add(new PiechartViewModel
            {
                Name = "Total Driver",
                Count = await _context.DriverDetails.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync()
            });

            // Total Members
            result.Add(new PiechartViewModel
            {
                Name = "Total Member",
                Count = await _context.Member.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync()
            });

            // Total Vehicles
            result.Add(new PiechartViewModel
            {
                Name = "Total Vehicle",
                Count = await _context.VehicleDarta.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync()
            });

            // Total Co-Drivers
            result.Add(new PiechartViewModel
            {
                Name = "Total Co-Driver",
                Count = await _context.BusconductorDetails.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync()
            });

            return result;
        }
        public async Task<List<PiechartViewModel>> GetPieDataNumberTwo()
        {
            var vecType = await _context.VehicleType.Select(x => new PiechartViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            foreach (var item in vecType)
            {
                item.Count = await _context.VehicleDarta.Where(x => x.VehicleTypeId == item.Id && x.OrganizationId == int.Parse(_userOrgId)).CountAsync();
            }
            return vecType;
        }
        public async Task<List<VehicleEngineTypeChartViewModel>> GetLineDataNumberThree()
        {
            var data = await _context.VehicleDarta.Where(x => x.OrganizationId == int.Parse(_userOrgId))
                .Include(v => v.VehicleCategory)
                .Include(v => v.EngineType)
                .ToListAsync();

            var groupedData = data
                .GroupBy(v => v.VehicleCategory.Name)
                .Select(group => new VehicleEngineTypeChartViewModel
                {
                    Category = group.Key,
                    EngineTypeCounts = group
                        .GroupBy(v => v.EngineType.Name)
                        .ToDictionary(
                            etGroup => etGroup.Key,
                            etGroup => etGroup.Count()
                        )
                })
                .ToList();

            return groupedData;
        }
        public async Task<List<BusRouteStopsViewModel>> GetLineDataNumberFour()
        {
            var routeList = await _context.BusRoute
                .Take(10)
                .ToListAsync();

            var routes = routeList.Select(route => new BusRouteStopsViewModel
            {
                RouteName = route.SourceName + " → " + route.DestinationName,
                Stops = _context.StopSetup
                    .Where(s => s.RouteId == route.Id)
                    .OrderBy(s => s.Id)
                    .Select(s => s.Name)
                    .ToList()
            }).ToList();

            return routes;
        }


        public async Task<bool> SendSms(int id, string text)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var aa = await SendMsg(id);
                    var number = aa;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response;

                    var parameters = $"token={_smsModel.Token}&from={_smsModel.From}" + $"&to={number}&text={text}";
                    //var parameters = string.Format("sender={0}&to={1}&message={2}&token={3}", "BiratnagarMun", number, text, _smsModel.Token);

                    response = await client.GetAsync("https://api.sparrowsms.com/v2/sms/?" + parameters);
                    var a = response.Content.ReadAsStringAsync().Result;

                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<string> SendMsg(int id)
        {
            var orgid = await _context.VehicleDarta.Where(x => x.Id == id).Select(x => x.OrganizationId).FirstOrDefaultAsync();
            var Mobile = await _context.OrganizationDetail.Where(x => x.Id == orgid).Select(x => x.PhoneNumber).FirstOrDefaultAsync();

            return Mobile;
        }
    }
}
