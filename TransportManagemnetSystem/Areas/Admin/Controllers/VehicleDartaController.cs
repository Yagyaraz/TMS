using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleDartaController : Controller
    {
        private readonly IVehicleDarta _VehicleDarta;
        private readonly string _userId;
        private readonly LoggingService _loggingService;
        private readonly IHttpContextAccessor _httpContext;
        public VehicleDartaController(IVehicleDarta VehicleDarta, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _VehicleDarta = VehicleDarta;
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        public async Task<IActionResult> VehicleDarta()
        {
            await _loggingService.LogAsync(_userId, "Viewed Vehicle Darta List");
            return View(await _VehicleDarta.GetAllVehicleDarta());
        }
        public async Task<IActionResult> CreateVehicleDarta(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Vehicle Darta Page");
            return View(await _VehicleDarta.GetVehicleDarta(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicleDarta(VehicleDartaViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _VehicleDarta.InsertUpdateVehicleDarta(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Vehicle Darta with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("VehicleDarta");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> ApproveVehicleDarta(int Id, string Name, string Darja, string Date, string approveNo)
        {
            await _loggingService.LogAsync(_userId, "Viewed Vehicle Darta Approved button clicked");
            return Json(await _VehicleDarta.InserApproveData(Id, Name, Darja, Date, approveNo));
        }

        [HttpPost]
        public async Task<JsonResult> ApproveAll(int Id, int DriverId, int ConductorId, int RouteId)
        {
            await _loggingService.LogAsync(_userId, "Vehicle, Route, Driver, and Bus Conductor assigned");
            return Json(await _VehicleDarta.InsertAssignData(Id, DriverId, ConductorId, RouteId));
        }
        public async Task<IActionResult> UploadFile(int Vehicleid = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Vehicle Darta upload button Accessed");
            var model = new VehicleDartaAddImageViewModel
            {
                VehicleDartaId = Vehicleid,
                FileList = await _VehicleDarta.GetFileById(Vehicleid)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(VehicleDartaAddImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _VehicleDarta.InsertFiles(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Vehicle Darta upload File with ID {model.Id}");

                    return RedirectToAction("VehicleDarta");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> VehicleDartaFiles()
        {
            await _loggingService.LogAsync(_userId, "Viewed Vehicle Darta File List");
            return View(await _VehicleDarta.GetAllVehicleDarta());
        }
        public async Task<IActionResult> VehicleDartaFilesById(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Vehicle Darta File Page");
            return View(await _VehicleDarta.VehicleDartaFiles(id));
        }

        public IActionResult DocsDetails() => PartialView("_VehicleFilePartial", new VehicleDartaAddImageViewModel());


        public async Task<IActionResult> BookingSeat(int Vehicleid)
        {
            await _loggingService.LogAsync(_userId, "Accessed Booking Seat Page");
            var data = await _VehicleDarta.BookingSeat(Vehicleid);
            ViewBag.VehicleId = Vehicleid;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> BookSeats([FromBody] BookingRequest request)
        {
            if (request == null || request.SeatNumbers == null || !request.SeatNumbers.Any())
            {
                await _loggingService.LogAsync(_userId, "Invalid request to book a seat.");
                return BadRequest("Invalid request. No seats selected.");
            }

            var result = await _VehicleDarta.BookSeatNow(request);

            if (result)
            {
                await _loggingService.LogAsync(_userId, "Seats booked successfully");
                return Json(new { success = true, message = "Seats booked successfully!" });
            }
            else
            {
                return BadRequest("Failed to book seats.");
            }
        }
        public async Task<IActionResult> MyBookedSeat()
        {
            await _loggingService.LogAsync(_userId, "Viewed My Booked Seat List");
            return View(await _VehicleDarta.GetAllBookedSeats());
        }
    }
}
