using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestController : Controller
    {
        private readonly TransportDbContext _context;

        public TestController(TransportDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRouteData(int routeId)
        {
            var route = await _context.BusRoute.FindAsync(routeId);
            var stops = await _context.StopSetup.Where(s => s.RouteId == routeId).ToListAsync();

            var routeData = new
            {
                RouteName = route.RouteName,
                Source = new
                {
                    Name = route.SourceName,
                    Latitude = route.SourceLatitude,
                    Longitude = route.SourceLongnitude
                },
                Destination = new
                {
                    Name = route.DestinationName,
                    Latitude = route.DestinationLatitude,
                    Longitude = route.DestinationLongnitude
                },
                Stops = stops.Select(s => new
                {
                    Name = s.Name,
                    Latitude = s.Latitude,
                    Longitude = s.Longnitude,
                    Time = s.Time,
                    StopTime = s.StopTime
                }).ToList()
            };

            return Json(routeData);
        }

        private static List<Seat> seats = new List<Seat>
    {
        new Seat { Id = 1, SeatNumber = "A1", IsBooked = false },
        new Seat { Id = 2, SeatNumber = "A2", IsBooked = false },
        new Seat { Id = 3, SeatNumber = "A3", IsBooked = true },
        new Seat { Id = 4, SeatNumber = "A4", IsBooked = false }
    };

        public IActionResult BookingIndex()
        {
            return View(seats);
        }

        [HttpPost]
        public IActionResult BookSeats(List<int> selectedSeatIds, string userName)
        {
            if (selectedSeatIds == null || !selectedSeatIds.Any())
            {
                TempData["Error"] = "Please select at least one seat.";
                return RedirectToAction("Index");
            }

            foreach (var seatId in selectedSeatIds)
            {
                var seat = seats.FirstOrDefault(s => s.Id == seatId);
                if (seat != null) seat.IsBooked = true;
            }

            TempData["Success"] = $"Seats booked successfully for {userName}!";
            return RedirectToAction("Index");
        }
    }
}