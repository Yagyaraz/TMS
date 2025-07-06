using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mono.TextTemplating;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Models;

namespace TransportManagemnetSystem.Utilities
{
    public class OrganizationDeatailRepository : IOrganizationDeatais
    {
        private readonly TransportDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<OrganizationDeatailRepository> _logger;
        private string userId = null;
        private readonly string _userOrgId = null;

        public OrganizationDeatailRepository(
            TransportDbContext context,
            ILogger<OrganizationDeatailRepository> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userOrgId = _httpContextAccessor.HttpContext.User.FindFirstValue("OrgId");

        }

        public async Task<bool> InsertUpdateOrganizationDeatais(OrganizationDetailViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    var data = await _context.OrganizationDetail.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (data != null) 
                    {
                        data.OrganizationName = model.OrganizationName;
                        data.OrganizationNameEng = model.OrganizationNameEng;
                        data.StateId = model.StateId;
                        data.DistrictId = model.DistrictId;
                        data.PalikaId = model.PalikaId;
                        data.WardNo = model.WardNo;
                        data.Area = model.Area;
                        data.Email = model.Email;
                        data.DartaMiti = model.DartaMiti;
                        data.DartaMitiEng = model.DartaMitiEng;
                        data.DartaLocation = model.DartaLocation;
                        data.PhoneNumber = model.PhoneNumber;

                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var dataa = new OrganizationDetail()
                    {
                        OrganizationName = model.OrganizationName,
                        OrganizationNameEng = model.OrganizationNameEng,
                        StateId = model.StateId,
                        DistrictId = model.DistrictId,
                        PalikaId = model.PalikaId,
                        WardNo = model.WardNo,
                        Area = model.Area,
                        Email = model.Email,
                        DartaMiti = model.DartaMiti,
                        DartaMitiEng = model.DartaMitiEng,
                        DartaLocation = model.DartaLocation,
                        PhoneNumber = model.PhoneNumber,
                        UserId = model.UserId,
                        Role = model.Role,
                    };
                    await _context.OrganizationDetail.AddAsync(dataa);
                    await _context.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {
                userId = string.IsNullOrWhiteSpace(userId) ? model.OrganizationName : userId;
                _logger.LogInformation("UserDetailsDepartment Repo create/update Error User Id = " + userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        public async Task<bool> InsertUpdateOrgUserDeatails(OrgUserViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    var data = await _context.OrganizationUserDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (data != null) 
                    {
                        data.OrgId = model.OrgId;
                        data.Name = model.Name;
                        data.MobileNo = model.MobileNo;
                        data.Address = model.Address;

                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var dataa = new OrganizationUserDetails()
                    {
                        OrgId = int.Parse(_userOrgId),
                        Name = model.Name,
                        MobileNo = model.MobileNo,
                        Address = model.Address,
                        Email = model.Email,
                        UserId = model.UserId,
                        Role = model.Role,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                    };
                    await _context.OrganizationUserDetails.AddAsync(dataa);
                    await _context.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {
                userId = string.IsNullOrWhiteSpace(userId) ? model.Name : userId;
                _logger.LogInformation("OrganizationUserDetails Repo create/update Error User Id = " + userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        public async Task<bool> InsertUpdateOuterUserDetail(OuterUserRegistrationViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    var data = await _context.OuterUserRegistration.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (data != null) 
                    {
                        data.FullName = model.FullName;
                        data.FullNameEng= model.FullNameEng;
                        data.StateId = model.StateId;
                        data.DistrictId = model.DistrictId;
                        data.PalikaId = model.PalikaId;
                        data.WardNo = model.WardNo;
                        data.Area = model.Area;
                        data.Email = model.Email;
                        data.CitizenIssueMitiEng = model.CitizenIssueMitiEng;
                        data.CitizenIssueMiti = model.CitizenIssueMiti;
                        data.CitizenNo = model.CitizenNo;
                        data.PhoneNumber = model.PhoneNumber;

                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var dataa = new OuterUserRegistration()
                    {
                        FullName = model.FullName,
                        FullNameEng = model.FullNameEng,
                        StateId = model.StateId,
                        DistrictId = model.DistrictId,
                        PalikaId = model.PalikaId,
                        WardNo = model.WardNo,
                        Area = model.Area,
                        Email = model.Email,
                        CitizenNo = model.CitizenNo,
                        CitizenIssueMiti = model.CitizenIssueMiti,
                        CitizenIssueMitiEng = model.CitizenIssueMitiEng,
                        PhoneNumber = model.PhoneNumber,
                        UserId = model.UserId,
                    };
                    await _context.OuterUserRegistration.AddAsync(dataa);
                    await _context.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {
                userId = string.IsNullOrWhiteSpace(userId) ? model.FullName : userId;
                _logger.LogInformation("UserDetailsDepartment Repo create/update Error User Id = " + userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
    }
}
