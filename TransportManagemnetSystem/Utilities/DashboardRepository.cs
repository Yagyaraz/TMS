using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Models;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Utilities
{
    public class DashboardRepository : IDashboard
    {
        private readonly TransportDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<DashboardRepository> _logger;
        private readonly string _userId = null;
        private readonly string _userOrgId = null;
        private readonly IUtility _utility;
        public DashboardRepository(TransportDbContext context, IHttpContextAccessor httpContext, ILogger<DashboardRepository> logger, IUtility utility)
        {
            _context = context;
            _logger = logger;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _utility = utility;
            _userOrgId = httpContext.HttpContext.User.FindFirstValue("OrgId");
        }
        public async Task<DashboardViewModel> GetDashBoardData()
        {
            var model = new DashboardViewModel();
            model.TotalList.Add(new TotalCountViewModel() { Name = "Total Committee", Count = await _context.OrganizationDetail.Where(x => x.Role == UserRoles.SubAdmin).CountAsync() });
            model.TotalList.Add(new TotalCountViewModel() { Name = "Total Member", Count = await _context.Member.CountAsync() });
            model.TotalList.Add(new TotalCountViewModel() { Name = "Total Vehicle", Count = await _context.VehicleDarta.CountAsync() });

            model.TotalListAccToOrg.Add(new TotalAccordingToOrgViewModel() { Name = "Total Driver", Count = await _context.DriverDetails.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync() });
            model.TotalListAccToOrg.Add(new TotalAccordingToOrgViewModel() { Name = "Total Member", Count = await _context.Member.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync() });
            model.TotalListAccToOrg.Add(new TotalAccordingToOrgViewModel() { Name = "Total Vehicle", Count = await _context.VehicleDarta.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync()});
            model.TotalListAccToOrg.Add(new TotalAccordingToOrgViewModel() { Name = "Total Co-Driver", Count = await _context.BusconductorDetails.Where(x => x.OrganizationId == int.Parse(_userOrgId)).CountAsync()});


            return model;
            //var model = new DashboardViewModel();
            //model.TotalList.Add(new TotalCountViewModel() { Name = "जम्मा समिति", Count = await _context.OrganizationDetail.Where(x => x.Role == UserRoles.SubAdmin).CountAsync() });
            //model.TotalList.Add(new TotalCountViewModel() { Name = "जम्मा सदस्य", Count = await _context.Member.CountAsync() });
            //model.TotalList.Add(new TotalCountViewModel() { Name = "जम्मा सवारी", Count = await _context.VehicleDarta.CountAsync() });
            //return model;
        }
    }
}
