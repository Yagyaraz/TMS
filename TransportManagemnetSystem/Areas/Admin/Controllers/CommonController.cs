using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommonController : Controller
    {
        private readonly ICommon _common;
        private readonly string _userId;
        private readonly LoggingService _loggingService;

        private readonly IHttpContextAccessor _httpContext;
        public CommonController(ICommon common, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _common = common;
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        #region fiscalyear
        public async Task<IActionResult> FiscalYear()
        {
            await _loggingService.LogAsync(_userId, "Viewed Fiscal Year List");
            return View(await _common.GetAllFiscalYear());
        }

        public async Task<IActionResult> CreateFiscalYear(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Fiscal Year Page");
            return View(await _common.GetFiscalYear(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFiscalYear(FiscalYearViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                                   .Select(x => new { x.Key, x.Value.Errors })
                                   .ToArray();

            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateFiscalYear(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Fiscal Year with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("FiscalYear");
                }
            }
            return View(model);
        }
        //public async Task<IActionResult> FiscalYear()
        //{
        //    return View(await _common.GetAllFiscalYear());
        //}
        //public async Task<IActionResult> CreateFiscalYear(int id = 0)
        //{
        //    return View(await _common.GetFiscalYear(id));
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateFiscalYear(FiscalYearViewModel model)
        //{
        //    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
        //    if (ModelState.IsValid)
        //    {
        //        if (await _common.InsertUpdateFiscalYear(model))
        //        {
        //            TempData["msg"] = "success";
        //            return RedirectToAction("FiscalYear");
        //        }
        //    }
        //    return View(model);
        //}
        #endregion
        #region Office
        public async Task<IActionResult> Office()
        {
            await _loggingService.LogAsync(_userId, "Viewed Office List");
            return View(await _common.GetAllOffice());
        }
        public async Task<IActionResult> CreateOffice(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Office Page");
            return View(await _common.GetOffice(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateOffice(OfficeViewModel model)
        {
            var action = model.Id == 0 ? "Created" : "Updated";
            await _loggingService.LogAsync(_userId, $"{action} Office with ID {model.Id}");
            if (!ModelState.IsValid) return View(model);
            if (!await _common.InsertUpdateOffice(model)) return View(model);
            TempData["msg"] = "success";
            return RedirectToAction("Office");
        }
        #endregion
        #region VehicleCategory
        public async Task<IActionResult> VehicleCategory()
        {
            await _loggingService.LogAsync(_userId, "Viewed Vehicle Category List");
            return View(await _common.GetAllVehicleCategory());
        }
        public async Task<IActionResult> CreateVehicleCategory(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Vehicle Category Page");
            return View(await _common.GetVehicleCategory(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicleCategory(VehicleCategoryViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateVehicleCategory(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Vehicle Category with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("VehicleCategory");
                }
            }
            return View(model);
        }
        #endregion

        #region VehicleType
        public async Task<IActionResult> VehicleType()
        {
            await _loggingService.LogAsync(_userId, "Viewed Vehicle Type List");
            return View(await _common.GetAllVehicleType());
        }
        public async Task<IActionResult> CreateVehicleType(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Vehicle Type Page");
            return View(await _common.GetVehicleType(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicleType(VehicleTypeViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateVehicleType(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Vehicle Type with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("VehicleType");
                }
            }
            return View(model);
        }
        #endregion
        #region EngineType
        public async Task<IActionResult> EngineType()
        {
            await _loggingService.LogAsync(_userId, "Viewed Engine Type List");
            return View(await _common.GetAllEngineType());
        }
        public async Task<IActionResult> CreateEngineType(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Engine Type Page");
            return View(await _common.GetEngineType(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateEngineType(EngineTypeViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateEngineType(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Engine Type with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("EngineType");
                }
            }
            return View(model);
        }
        #endregion
        #region ManufactureCompany
        public async Task<IActionResult> ManufactureCompany()
        {
            await _loggingService.LogAsync(_userId, "Viewed Manufacture Company Type List");
            return View(await _common.GetAllManufactureCompany());
        }
        public async Task<IActionResult> CreateManufactureCompany(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Manufacture Company Page");
            return View(await _common.GetManufactureCompany(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateManufactureCompany(ManufactureCompanyViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateManufactureCompany(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Manufacture Company with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("ManufactureCompany");
                }
            }
            return View(model);
        }
        #endregion
        #region StandardType
        public async Task<IActionResult> StandardType()
        {
            await _loggingService.LogAsync(_userId, "Viewed Standard Type List");
            return View(await _common.GetAllStandardType());
        }
        public async Task<IActionResult> CreateStandardType(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Standard Type Page");
            return View(await _common.GetStandardType(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateStandardType(StandardTypeViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateStandardType(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Standard Type with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("StandardType");
                }
            }
            return View(model);
        }
        #endregion
        #region FAQ
        public async Task<IActionResult> FAQList()
        {
            await _loggingService.LogAsync(_userId, "Viewed FAQ List");
            return View(await _common.GetALlFAQ());
        }
        public async Task<IActionResult> CreateFAQ(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create FAQ Page");
            return View(await _common.GetFAQById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateFAQ(FAQViewModel model)
        {
            var action = model.Id == 0 ? "Created" : "Updated";
            await _loggingService.LogAsync(_userId, $"{action} FAQ with ID {model.Id}");
            if (!ModelState.IsValid) return View(model);
            if (!await _common.InsertUpdateFAQ(model)) return View(model);
            TempData["msg"] = "success";
            return RedirectToAction("FAQList");
        }
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            if (await _common.DeleteFAQ(id))
            {
                TempData["msg"] = "Successfully Deleted FAQ";
                await _loggingService.LogAsync(_userId, "FAQ Deleted By userID" + _userId);
            }
            else
            {
                TempData["msg"] = "Error while deleting FAQ";
                await _loggingService.LogAsync(_userId, "FAQ  tried to Delete By userID" + _userId +"but unsuccessful");
            }
            return RedirectToAction("FAQList");
        }
        #endregion
    }
}
