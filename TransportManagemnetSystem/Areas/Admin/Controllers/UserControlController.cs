using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserControlController : Controller
    {
        private readonly IUserControl _userControl;
        private readonly string _userId;
        private readonly LoggingService _loggingService;
        private readonly IHttpContextAccessor _httpContext;
        public UserControlController(IUserControl userControl, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _userControl = userControl;
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        public async Task<IActionResult> UserList()
        {
            await _loggingService.LogAsync(_userId, "Viewed User List");
            return View(await _userControl.GetUserList());
        }
        
        public async Task<IActionResult> OrgUserList()
        {
            await _loggingService.LogAsync(_userId, "Viewed OrgUser List");
            return View(await _userControl.GetOrgUserList());
        }

        public async Task<IActionResult> UpdateUser(string id)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create User Page");
            return View(await _userControl.GetUserById(id));
        }

        public async Task<IActionResult> ResetPassword(string id)
        {
            await _loggingService.LogAsync(_userId, "Accessed Reset Password");
            return Json(await _userControl.ResetUserPassword(id));
        }
        public async Task<IActionResult> UserActivateOrDeactivate(string id)
        {
            await _loggingService.LogAsync(_userId, "Accessed User Activate Or Deactivate");
            return Json(await _userControl.UserActivateOrDeactivate(id));
        }
        public async Task<IActionResult> ChangeRole(string id, string role)
        {
            await _loggingService.LogAsync(_userId, "Accessed Change Role");
            return Json(await _userControl.AssignOrChangeRole(id, role));
        }

        public async Task<IActionResult> UserLogDetails(string userid)
        {
            await _loggingService.LogAsync(_userId, "Accessed User Log Details Page");
            ViewBag.userid = userid;
            return View(await _userControl.GetAllUserDetail(userid));
        }
    }
}
