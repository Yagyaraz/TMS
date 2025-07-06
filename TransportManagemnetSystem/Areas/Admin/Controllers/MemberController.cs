using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Security;

namespace TransportManagemnetSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly IMember _Member;
        private readonly string _userId;
        private readonly LoggingService _loggingService;
        private readonly IHttpContextAccessor _httpContext;
        public MemberController(IMember member, IHttpContextAccessor httpContext, LoggingService loggingService)
        {
            _Member = member; 
            _loggingService = loggingService;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }

        public async Task<IActionResult> Member()
        {
            await _loggingService.LogAsync(_userId, "Viewed Member List");
            return View(await _Member.GetAllMember());
        }
        public async Task<IActionResult> CreateMember(int id = 0)
        {
            await _loggingService.LogAsync(_userId, "Accessed Create Member Page");
            return View(await _Member.GetMemberById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateMember(MemberViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _Member.InsertUpdateMember(model))
                {
                    var action = model.Id == 0 ? "Created" : "Updated";
                    await _loggingService.LogAsync(_userId, $"{action} Member with ID {model.Id}");
                    TempData["msg"] = "success";
                    return RedirectToAction("Member");
                }
            }
            return View(model);
        }
    }
}
