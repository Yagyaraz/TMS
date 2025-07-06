using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IReport _Report;
        private readonly string _userId;
        private readonly LoggingService _loggingService;
        private readonly IHttpContextAccessor _httpContext;
        public ReportController(IReport report, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _Report = report;
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        public async Task<IActionResult> OrginizationList()
        {
            await _loggingService.LogAsync(_userId, "Viewed Orginization List");
            return View(await _Report.GetAllOrganixzation());
        }
        public async Task<IActionResult> OrginizationDetailById(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Orginization Page");
            return View(await _Report.GetAllOrganixzationById(id));
        }
        public async Task<IActionResult> RouteSearch(string source, string destination)
        {
            await _loggingService.LogAsync(_userId, "Accessed Route Search Page");
            ViewBag.source = source;
            ViewBag.destination = destination;
            return View(await _Report.RouteSearch(source, destination));
        }
        public async Task<IActionResult> RouteDetailsById(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Route Details Page");
            return View(await _Report.RouteDetailsById(id));
        }
        public async Task<IActionResult> OutRouteSearch(string source, string destination)
        {
            await _loggingService.LogAsync(_userId, "Accessed Out Route Search Page");
            ViewBag.source = source;
            ViewBag.destination = destination;
            return View(await _Report.OutRouteSearch(source, destination));
        }
        public async Task<IActionResult> OutRouteSearchDetailsById(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Out Route Details Page");
            return View(await _Report.OutRouteDetailsById(id));
        }
        public async Task<IActionResult> VehicleDartaList()
        {
            await _loggingService.LogAsync(_userId, "Viewed VehicleDarta List");
            return View(await _Report.VehicleDartaList());
        }
        public async Task<IActionResult> BusDetail(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Viewed VehicleDarta Details");
            return View(await _Report.VehicleDartaDetailsById(id));
        }
        public async Task<IActionResult> VehicleDartaDisApppList()
        {
            await _loggingService.LogAsync(_userId, "Viewed VehicleDarta Disapproved List");
            return View(await _Report.VehicleDartaList());
        }
        public async Task<IActionResult> DriverDetailReport()
        {
            await _loggingService.LogAsync(_userId, "Viewed DriverDetailReport List");
            return View(await _Report.DriverList());
        }
        public async Task<IActionResult> DriverDetailReportById(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Viewed Driver Full Details");
            return View(await _Report.DriverDetailReportById(id));
        }
        public async Task<IActionResult> CoDriverDetailReport()
        {
            await _loggingService.LogAsync(_userId, "Viewed CoDriverDetailReport List");
            return View(await _Report.CoDriverList());
        }
        public async Task<IActionResult> CoDriverDetailReportById(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Viewed CoDriver Full Details");
            return View(await _Report.CoDriverDetailReportById(id));
        }
        public IActionResult Anusuchi4()
        {
            return View();
        }
        public IActionResult Anusuchi5()
        {
            return View();
        }
        public IActionResult Anusuchi6()
        {
            return View();
        }
        public IActionResult Anusuchi7()
        {
            return View();
        }
        public IActionResult Anusuchi8()
        {
            return View();
        }
        public IActionResult Anusuchi9()
        {
            return View();
        }
        public IActionResult Anusuchi13()
        {
            return View();
        }
        public IActionResult Anusuchi14()
        {
            return View();
        }
        public IActionResult Anusuchi15()
        {
            return View();
        }
        public IActionResult Anusuchi17()
        {
            return View();
        }

    }
}
