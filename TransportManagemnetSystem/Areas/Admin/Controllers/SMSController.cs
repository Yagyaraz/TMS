using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SMSController : Controller
    {
        private readonly IUserControl _userControl;
        private readonly string _userId;
        private readonly LoggingService _loggingService;
        private readonly IHttpContextAccessor _httpContext;
        public SMSController(IUserControl userControl, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _userControl = userControl;
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Archival()
        {
            return View();
        }
        public async Task<IActionResult> UserManagement()
        {
            await _loggingService.LogAsync(_userId, "Viewed UserManagement List");
            return View(await _userControl.GetUserList());
        }
        public async Task<IActionResult> UserDetails(string id)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create UserDetails Page");
            return View(await _userControl.UserDetailsById(id));
        }
        public IActionResult DataEntry()
        {
            return View();
        }
        public IActionResult AudiTrial()
        {
            return View();
        } 
        public IActionResult RegistrationConfirmation()
        {
            return View();
        } 
        public IActionResult RenewalReminders()
        {
            return View();
        }
        public IActionResult FeePaymentNotifications()
        {
            return View();
        } 
        public IActionResult ApplicationStatusUpdates()
        {
            return View();
        }  
        public IActionResult SMSBasedQuerySystem()
        {
            return View();
        }
        public IActionResult CustomCommands()
        {
            return View();
        } 
        public IActionResult OTPforVerification()
        {
            return View();
        } 
        public IActionResult GDPRPrivacyCompliance()
        {
            return View();
        }
    }
}
