using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Security;
using TransportManagemnetSystem.Utilities;

namespace TransportManagemnetSystem.Areas.Admin.Repositories
{
    public class MemberRepository : IMember
    {
        private readonly IUtility _utility;
        private readonly TransportDbContext _context;
        private readonly ILogger<MemberRepository> _logger;
        private readonly string _userId;
        private readonly IHttpContextAccessor _httpContext;
        public MemberRepository(TransportDbContext context, IHttpContextAccessor httpContext, ILogger<MemberRepository> logger, IUtility utility)
        {
            _context = context;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger = logger;
            _utility = utility;
        }

        public async Task<List<MemberViewModel>> GetAllMember()
        {
            var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.Member.AsQueryable();

            //if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            //{
            //    query = query.Where(x => x.OrganizationDetail.StateId == int.Parse(_userStateId));
            //}
            if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => orgid == 0 || x.OrganizationId == orgid);
            }
            return await query
                  .Select(x => new MemberViewModel()
                  {
                      Id = x.Id,
                      Address = x.Address,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDate = x.CitizenIssueDate,
                      CitizenIssueDistricrId = x.CitizenIssueDistricrId,
                      Email = x.Email,
                      FaxNo = x.FaxNo,
                      FullName = x.FullName,
                      FullNameEng = x.FullNameEng,
                      MobileNo = x.MobileNo,
                      OrganizationId = x.OrganizationId,
                      PanVatNo = x.PanVatNo,
                      RegNo = x.RegNo,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                  }).ToListAsync();
        }

        public async Task<MemberViewModel> GetMemberById(int id)
        {
            return await _context.Member.Where(x => x.Id == id)
                  .Select(x => new MemberViewModel()
                  {
                      Id = x.Id,
                      Address = x.Address,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDate = x.CitizenIssueDate,
                      CitizenIssueDistricrId = x.CitizenIssueDistricrId,
                      Email = x.Email,
                      FaxNo = x.FaxNo,
                      FullName = x.FullName,
                      FullNameEng = x.FullNameEng,
                      MobileNo = x.MobileNo,
                      OrganizationId = x.OrganizationId,
                      PanVatNo = x.PanVatNo,
                      RegNo = x.RegNo,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                  }).FirstOrDefaultAsync() ?? new MemberViewModel();
        }

        public async Task<bool> InsertUpdateMember(MemberViewModel model)
        {
            try
            {
                var PPPhotoo = "";
                var CitizenPhotoo = "";

                if (model.Id > 0)
                {
                    var data = await _context.Member.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (model.PPFilePath != null)
                    {
                        PPPhotoo = _utility.UploadImgReturnPathAndName("Member", model.PPFilePath, "ProfilePhoto").Result.FilePath;
                        if (data.PPPhotoPath != null)
                        {
                            await _utility.RemoveFileFormServer(data.PPPhotoPath);
                        }
                    }
                    if (model.CitizenFilePath != null)
                    {
                        CitizenPhotoo = _utility.UploadImgReturnPathAndName("Member", model.CitizenFilePath, "CitizenPhoto").Result.FilePath;
                        if (data.CitizenPhotoPath != null)
                        {
                            await _utility.RemoveFileFormServer(data.CitizenPhotoPath);
                        }
                    }
                    if (data != null)
                    {
                        data.FullName = model.FullName;
                        data.FullNameEng = model.FullNameEng;
                        data.CitizenNo = model.CitizenNo;
                        data.CitizenIssueDate = model.CitizenIssueDate;
                        data.CitizenIssueDistricrId = model.CitizenIssueDistricrId;
                        data.RegNo = model.RegNo;
                        data.PanVatNo = model.PanVatNo;
                        data.Address = model.Address;
                        data.Email = model.Email;
                        data.MobileNo = model.MobileNo;
                        data.FaxNo = model.FaxNo;
                        data.PPPhotoPath = PPPhotoo == "" ? data.PPPhotoPath : PPPhotoo;
                        data.CitizenPhotoPath = CitizenPhotoo == "" ? data.CitizenPhotoPath : CitizenPhotoo;

                        data.UpdatedBy = _userId;
                        data.UpdatedDate = DateTime.Now;

                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else return false;
                }
                else
                {
                    if (model.PPFilePath != null)
                    {
                        PPPhotoo = _utility.UploadImgReturnPathAndName("Member", model.PPFilePath, "ProfilePhoto").Result.FilePath;
                    }
                    if (model.CitizenFilePath != null)
                    {
                        CitizenPhotoo = _utility.UploadImgReturnPathAndName("Member", model.CitizenFilePath, "CitizenPhoto").Result.FilePath;
                    }
                    var orgid = await _utility.GetLoggedInOrgId();
                    if (orgid > 0)
                    {
                        var dataa = new Member()
                        {
                           OrganizationId = orgid,
                           FullName = model.FullName,
                           FullNameEng = model.FullNameEng,
                           CitizenNo = model.CitizenNo,
                           CitizenIssueDate = model.CitizenIssueDate,
                           CitizenIssueDistricrId = model.CitizenIssueDistricrId,
                           RegNo = model.RegNo,
                           PanVatNo = model.PanVatNo,
                           Address = model.Address,
                           Email = model.Email,
                           MobileNo = model.MobileNo,
                           FaxNo = model.FaxNo,
                           PPPhotoPath = PPPhotoo,
                           CitizenPhotoPath = CitizenPhotoo,

                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.Member.AddAsync(dataa);
                        await _context.SaveChangesAsync();
                    }
                    else return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Member Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
    }
}
