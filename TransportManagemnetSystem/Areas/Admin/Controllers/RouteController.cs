using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RouteController : Controller
    {
        private readonly IRoute _Route;
        private readonly string _userId;
        private readonly LoggingService _loggingService;
        private readonly IHttpContextAccessor _httpContext;
        public RouteController(IRoute route, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _Route = route;
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        public async Task<IActionResult> Route()
        {
            await _loggingService.LogAsync(_userId, "Viewed Route List");
            return View(await _Route.GetAllRoute());
        }
        public async Task<IActionResult> CreateRoute(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Route Page");
            return View(await _Route.GetRouteById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoute(RouteViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _Route.InsertUpdateRoute(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Route with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("Route");
                }
            }
            return View(model);
        }
        public IActionResult StopPartial() => PartialView("_StopPartial", new StopSetupViewModel());


        public async Task<IActionResult> OutRoute()
        {
            await _loggingService.LogAsync(_userId, "Viewed OutRoute List");
            return View(await _Route.GetAllOutRoute());
        }
        public async Task<IActionResult> CreateOutRoute(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create OutRoute Page");
            return View(await _Route.GetOutRouteById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateOutRoute(OutRouteViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _Route.InsertUpdateOutRoute(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Route with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("OutRoute");
                }
            }
            return View(model);
        }

    }
}
