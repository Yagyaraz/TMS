using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TransportManagemnetSystem.Models;
using TransportManagemnetSystem.Utilities;

namespace TransportManagemnetSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboard _dashboard;

        public HomeController(ILogger<HomeController> logger, IDashboard dashboard)
        {
            _logger = logger;
            _dashboard = dashboard;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dashboard.GetDashBoardData());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> AdminDashobard()
        {
            return View();
        }
    }
}
