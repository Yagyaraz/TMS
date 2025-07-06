using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Models;
using TransportManagemnetSystem.Security;
using TransportManagemnetSystem.Utilities;

namespace TransportManagemnetSystem.Areas.Admin.Repositories
{
    public class UserControlRepository : IUserControl
    {
        private readonly TransportDbContext _context;
        private readonly IUtility _utility;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor httpContext;
        private readonly string _userStateId;
        private readonly ILogger<TransportDbContext> _logger;
        private readonly string _userId = null;
        private readonly string _userOrgId = null;


        public UserControlRepository(TransportDbContext context, IUtility utility, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext, ILogger<TransportDbContext> logger)
        {
            _context = context;
            _utility = utility;
            _userManager = userManager;
            this.httpContext = httpContext;
            _userStateId = httpContext.HttpContext.User.FindFirstValue("StateId");
            _logger = logger;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userOrgId = httpContext.HttpContext.User.FindFirstValue("OrgId");

        }
        public async Task<List<UserViewModel>> GetUserList()
        {

            if (!httpContext.HttpContext.User.IsInRole(UserRoles.Administrator) && !httpContext.HttpContext.User.IsInRole(UserRoles.SuperAdmin))
            {
                //var user = _utility.GetLoginUserDetails();
                var userId = httpContext.HttpContext.User.IsInRole(UserRoles.User) ? httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
                return await _context.Users
                    .Where(x => !x.UserName.Equals("softech@gmail.com")
                    && !x.UserName.Equals("superadmin@gmail.com")
                    && (userId == null || x.Id == userId)
                    ).Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Email,
                        UserName = x.UserName,
                        //WardId = x.WardId,
                        //WardName = _context.Ward.Where(z => z.Id == x.WardId).Select(z => z.Name).FirstOrDefault(),
                        Role = (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id where ur.UserId == x.Id select r.Name).FirstOrDefault(),
                        Active = (x.LockoutEnd ?? DateTime.UtcNow) <= DateTime.UtcNow
                    }).ToListAsync();
            }

            return await _context.Users.Where(x => !x.UserName.Equals("softech@gmail.com") && !x.UserName.Equals("superadmin@gmail.com"))
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Name = x.Email,
                    //WardId = x.WardId,
                    //WardName = _context.Ward.Where(z => z.Id == x.WardId).Select(z => z.Name).FirstOrDefault(),
                    UserName = x.UserName,
                    Role = (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id where ur.UserId == x.Id select r.Name).FirstOrDefault(),
                    Active = (x.LockoutEnd ?? DateTime.UtcNow) <= DateTime.UtcNow
                }).ToListAsync();
        }
        
        public async Task<List<OrgUserViewModel>> GetOrgUserList()
        {
            var data = await _context.OrganizationUserDetails.Where(x => x.OrgId == int.Parse(_userOrgId))
                .Select(x => new OrgUserViewModel()
                {
                    Id = x.Id,
                    OrgId = x.OrgId,
                    Name = x.Name,  
                    Email = x.Email,
                    MobileNo = x.MobileNo,
                    Address = x.Address,
                }).ToListAsync() ?? new List<OrgUserViewModel>();
            return data;
        }


        public async Task<OrganizationDetailViewModel> UserDetailsById(string id)
        {           
            return await _context.OrganizationDetail.Where(x => x.UserId == id)
                .Select(x => new OrganizationDetailViewModel() 
                { 
                    Id = x.Id,
                    OrganizationName = x.OrganizationName,
                    OrganizationNameEng = x.OrganizationNameEng,
                    StateId = x.StateId,
                    DistrictId = x.DistrictId,
                    PalikaId = x.PalikaId,
                    WardNo = x.WardNo,
                    Area = x.Area,
                    Email = x.Email,
                    DartaMiti = x.DartaMiti,
                    DartaLocation = x.DartaLocation,
                    PhoneNumber = x.PhoneNumber,
                    StateName = _context.State.Where(z => z.Id == x.StateId).Select(z => z.StateNameNep).FirstOrDefault(),
                    DistrictName = _context.District.Where(z => z.Id == x.DistrictId).Select(z => z.DistrictNameNep).FirstOrDefault(),
                    PalikaName = _context.Palika.Where(z => z.Id == x.PalikaId).Select(z => z.PalikaNameNep).FirstOrDefault(),
                }).FirstOrDefaultAsync() ?? new OrganizationDetailViewModel();
        }
        public async Task<bool> ResetUserPassword(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, "NewPass@123");
                if (result.Succeeded)
                {
                    _logger.LogInformation("Reset password - UserId = " + id + " Updated by - " + _userId + " Date : " + DateTime.Now);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Reset password - Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        public async Task<bool> AssignOrChangeRole(string id, string role)
        {
            try
            {
                var roles = await _context.UserRoles.Where(x => x.UserId == id).ToListAsync();
                foreach (var item in roles)
                {
                    _context.UserRoles.Remove(item);
                    await _context.SaveChangesAsync();
                }
                var user = await _userManager.FindByIdAsync(id);
                var newUserRole = _userManager.AddToRoleAsync(user, role);
                newUserRole.Wait();

                if (newUserRole.IsCompletedSuccessfully)
                {
                    var data = await _context.OrganizationDetail.Where(x => x.UserId == id).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.Role = role;

                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    _logger.LogInformation("Assign Or Change Role - UserId = " + id + " Updated by - " + _userId + " Date : " + DateTime.Now);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Assign Or Change Role - Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        public async Task<int> UserActivateOrDeactivate(string id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    if (user.LockoutEnd == null)
                    {
                        user.LockoutEnd = DateTimeOffset.MaxValue;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("User Deactive - UserId = " + id + " Updated by - " + _userId + " Date : " + DateTime.Now);
                        return 2;
                    }
                    else
                    {
                        user.LockoutEnd = null;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("User Active - UserId = " + id + " Updated by - " + _userId + " Date : " + DateTime.Now);
                        return 1;
                    }
                }
                _logger.LogInformation("User not found - UserId = " + id + " Updated by - " + _userId + " Date : " + DateTime.Now);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("User Active/DeActive - Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return 0;
            }
        }

        public async Task<UserViewModel> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    UserViewModel data = new UserViewModel()
                    {
                        Name = user.FullName,
                        Role = await (from r in _context.Roles
                                      join ur in _context.UserRoles on r.Id equals ur.RoleId
                                      join u in _context.Users on ur.UserId equals u.Id
                                      select r.Name).FirstOrDefaultAsync(),
                        //Post = user.Post,
                        PhoneNo = user.PhoneNumber,
                        //Address = user.Address,
                        Email = user.Email,
                        //WardId = user.WardId,
                        Password = user.PasswordHash,
                        //OfficerName = user.OfficerName,



                    };
                    return data;
                }
                else
                {
                    return new UserViewModel();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Failed to Retrieve user according to " + id + "");
                return new UserViewModel();
            }
        }

        public async Task<List<UserLogViewModel>> GetAllUserDetail(string userid)
        {
            return await _context.UserLog.Where(x => (userid == null || x.Username == userid))
                .Select(x => new UserLogViewModel() { 
                    Action = x.Action,
                    Id = x.Id,
                    Timestamp = x.Timestamp,
                    Username = _context.Users.Where(z => z.Id == x.Username).Select(z => z.UserName).FirstOrDefault(),
                }).OrderByDescending(x => x.Id).ToListAsync();
        }
    }
}
