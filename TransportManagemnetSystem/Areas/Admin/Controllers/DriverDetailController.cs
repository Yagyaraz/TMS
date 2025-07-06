using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DriverDetailController : Controller
    {
        private readonly IDriverDetail _DriverDetail;
        private readonly string _userId;
        private readonly LoggingService _loggingService;
        private readonly IHttpContextAccessor _httpContext;
        public DriverDetailController(IDriverDetail DriverDetail, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _DriverDetail = DriverDetail;
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        #region Driver
        public async Task<IActionResult> DriverDetail()
        {
            await _loggingService.LogAsync(_userId, "Viewed DriverDetail List");
            return View(await _DriverDetail.GetAllDriverDetail());
        }
        public async Task<IActionResult> CreateDriverDetail(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create DriverDetail Page");
            return View(await _DriverDetail.GetDriverDetailById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateDriverDetail(DriverDetailViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
            if (ModelState.IsValid)
            {
                if (await _DriverDetail.InsertUpdateDriverDetail(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} DriverDetail with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("DriverDetail");
                }
            }
            return View(model);
        } 
        #endregion
        #region Busconductor
        public async Task<IActionResult> BusconductorDetail()
        {
            await _loggingService.LogAsync(_userId, "Viewed BusconductorDetail List");
            return View(await _DriverDetail.GetAllBusconductorDetail());
        }
        public async Task<IActionResult> CreateBusconductorDetail(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create BusconductorDetail Page");
            return View(await _DriverDetail.GetBusconductorDetailById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateBusconductorDetail(BusconductorDetailViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
            if (ModelState.IsValid)
            {
                if (await _DriverDetail.InsertUpdateBusconductorDetail(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} BusconductorDetail with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("BusconductorDetail");
                }
            }
            return View(model);
        } 
        #endregion
    }
}
