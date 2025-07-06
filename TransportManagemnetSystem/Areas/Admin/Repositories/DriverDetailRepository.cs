using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Security;
using TransportManagemnetSystem.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransportManagemnetSystem.Areas.Admin.Repositories
{
    public class DriverDetailRepository : IDriverDetail
    {
        private readonly IUtility _utility;
        private readonly TransportDbContext _context;
        private readonly ILogger<DriverDetailRepository> _logger;
        private readonly string _userId;
        private readonly IHttpContextAccessor _httpContext;
        public DriverDetailRepository(TransportDbContext context, IHttpContextAccessor httpContext, ILogger<DriverDetailRepository> logger, IUtility utility)
        {
            _context = context;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger = logger;
            _utility = utility;
        }
       
        #region Driver
        public async Task<List<DriverDetailViewModel>> GetAllDriverDetail()
        {
            var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.DriverDetails.AsQueryable();

            //if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            //{
            //    query = query.Where(x => x.DriverDetails.StateId == int.Parse(_userStateId));
            //}
            if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => orgid == 0 || x.OrganizationId == orgid);
            }
            return await query
                  .Select(x => new DriverDetailViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      DistrictId = x.DistrictId,
                      PalikaId = x.PalikaId,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                      Relation = x.Relation,
                      Name = x.Name,
                      Contactno = x.Contactno,
                      DocPath = x.DocPath,
                      LicenseNo = x.LicenseNo,
                      DOE = x.DOE,
                  }).ToListAsync();
        }
        public async Task<DriverDetailViewModel> GetDriverDetailById(int id)
        {
            return await _context.DriverDetails.Where(x => x.Id == id)
                  .Select(x => new DriverDetailViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      DistrictId = x.DistrictId,
                      PalikaId = x.PalikaId,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                      Relation = x.Relation,
                      Name = x.Name,
                      Contactno = x.Contactno,
                      DocPath = x.DocPath,
                      LicenseNo = x.LicenseNo,
                      DOE = x.DOE,
                      LisenceCatList = _context.DriverLicenseCategory.Where(z => z.DriverDetailsId == x.Id).Select(z => z.LicenseId).ToList(),
                  }).FirstOrDefaultAsync() ?? new DriverDetailViewModel();
        }
        public async Task<bool> InsertUpdateDriverDetail(DriverDetailViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var PPPhotoo = "";
                    var CitizenPhotoo = "";
                    var Photoo = "";

                    if (model.Id > 0)
                    {
                        var data = await _context.DriverDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                        if (model.PPFilePath != null)
                        {
                            PPPhotoo = _utility.UploadImgReturnPathAndName("DriverDetail", model.PPFilePath, "ProfilePhoto").Result.FilePath;
                            if (data.PPPhotoPath != null)
                            {
                                await _utility.RemoveFileFormServer(data.PPPhotoPath);
                            }
                        }
                        if (model.CitizenFilePath != null)
                        {
                            CitizenPhotoo = _utility.UploadImgReturnPathAndName("DriverDetail", model.CitizenFilePath, "CitizenPhoto").Result.FilePath;
                            if (data.CitizenPhotoPath != null)
                            {
                                await _utility.RemoveFileFormServer(data.CitizenPhotoPath);
                            }
                        }
                        if (model.RelationFilePath != null)
                        {
                            Photoo = _utility.UploadImgReturnPathAndName("DriverDetail", model.RelationFilePath, "RelationDocs").Result.FilePath;
                            if (data.DocPath != null)
                            {
                                await _utility.RemoveFileFormServer(data.DocPath);
                            }
                        }
                        if (data != null)
                        {
                            data.FullName = model.FullName;
                            data.FullNameNep = model.FullNameNep;
                            data.GenderId = model.GenderId;
                            data.MobileNo = model.MobileNo;
                            data.PanNo = model.PanNo;
                            data.CitizenNo = model.CitizenNo;
                            data.CitizenIssueDistrict = model.CitizenIssueDistrict;
                            data.JariMiti = model.JariMiti;
                            data.GrandFatherName = model.GrandFatherName;
                            data.FatherName = model.FatherName;
                            data.StateId = model.StateId;
                            data.DistrictId = model.DistrictId;
                            data.PalikaId = model.PalikaId;
                            data.WardId = model.WardId;
                            data.PPPhotoPath = PPPhotoo == "" ? data.PPPhotoPath : PPPhotoo;
                            data.CitizenPhotoPath = CitizenPhotoo == "" ? data.CitizenPhotoPath : CitizenPhotoo;
                            data.DocPath = Photoo == "" ? data.DocPath : Photoo;
                            data.Relation = model.Relation;
                            data.Name = model.Name;
                            data.Contactno = model.Contactno;
                            data.LicenseNo = model.LicenseNo;
                            data.DOE = model.DOE;

                            data.UpdatedBy = _userId;
                            data.UpdatedDate = DateTime.Now;

                            _context.Entry(data).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            if (model.LisenceCatList.Count() > 0)
                            {
                                var resultSet = _context.DriverLicenseCategory.Where(x => x.DriverDetailsId == model.Id).ToList();
                                if (resultSet.Count > 0)
                                {
                                    foreach (var item in resultSet)
                                    {
                                        _context.DriverLicenseCategory.Remove(item);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                if (model.LisenceCatList != null)
                                {
                                    foreach (var item in model.LisenceCatList)
                                    {
                                        var data1 = new DriverLicenseCategory()
                                        {
                                            DriverDetailsId = data.Id,
                                            LicenseId = item,

                                            UpdatedBy = _userId,
                                            UpdatedDate = DateTime.Now,
                                        };
                                        await _context.DriverLicenseCategory.AddAsync(data1);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                        else return false;
                    }
                    else
                    {
                        if (model.PPFilePath != null)
                        {
                            PPPhotoo = _utility.UploadImgReturnPathAndName("DriverDetail", model.PPFilePath, "ProfilePhoto").Result.FilePath;
                        }
                        if (model.CitizenFilePath != null)
                        {
                            CitizenPhotoo = _utility.UploadImgReturnPathAndName("DriverDetail", model.CitizenFilePath, "CitizenPhoto").Result.FilePath;
                        }
                        if (model.RelationFilePath != null)
                        {
                            Photoo = _utility.UploadImgReturnPathAndName("DriverDetail", model.RelationFilePath, "RelationDocs").Result.FilePath;
                        }
                        var orgid = await _utility.GetLoggedInOrgId();
                        if (orgid > 0)
                        {
                            var dataa = new DriverDetails()
                            {
                                OrganizationId = orgid,
                                FullName = model.FullName,
                                FullNameNep = model.FullNameNep,
                                GenderId = model.GenderId,
                                MobileNo = model.MobileNo,
                                PanNo = model.PanNo,
                                CitizenNo = model.CitizenNo,
                                CitizenIssueDistrict = model.CitizenIssueDistrict,
                                JariMiti = model.JariMiti,
                                GrandFatherName = model.GrandFatherName,
                                FatherName = model.FatherName,
                                StateId = model.StateId,
                                DistrictId = model.DistrictId,
                                PalikaId = model.PalikaId,
                                WardId = model.WardId,
                                PPPhotoPath = PPPhotoo,
                                CitizenPhotoPath = CitizenPhotoo,

                                DocPath = Photoo,
                                Relation = model.Relation,
                                Name = model.Name,
                                Contactno = model.Contactno,
                                LicenseNo = model.LicenseNo,
                                DOE = model.DOE,

                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.DriverDetails.AddAsync(dataa);
                            await _context.SaveChangesAsync();

                            if (model.LisenceCatList != null)
                            {
                                foreach (var item in model.LisenceCatList)
                                {
                                    var data1 = new DriverLicenseCategory()
                                    {

                                        DriverDetailsId = dataa.Id,
                                        LicenseId = item,

                                        CreatedBy = _userId,
                                        CreatedDate = DateTime.Now,
                                    };
                                    await _context.DriverLicenseCategory.AddAsync(data1);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                        else return false;
                    }
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("DriverDetail Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        } 
        #endregion
        #region Busconductor
        public async Task<List<BusconductorDetailViewModel>> GetAllBusconductorDetail()
        {
            var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.BusconductorDetails.AsQueryable();

            //if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            //{
            //    query = query.Where(x => x.BusconductorDetails.StateId == int.Parse(_userStateId));
            //}
            if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => orgid == 0 || x.OrganizationId == orgid);
            }
            return await query
                  .Select(x => new BusconductorDetailViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      DistrictId = x.DistrictId,
                      PalikaId = x.PalikaId,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                  }).ToListAsync();
        }
        public async Task<BusconductorDetailViewModel> GetBusconductorDetailById(int id)
        {
            return await _context.BusconductorDetails.Where(x => x.Id == id)
                  .Select(x => new BusconductorDetailViewModel()
                  {
                      Id = x.Id,
                      FullName = x.FullName,
                      FullNameNep = x.FullNameNep,
                      GenderId = x.GenderId,
                      MobileNo = x.MobileNo,
                      PanNo = x.PanNo,
                      CitizenNo = x.CitizenNo,
                      CitizenIssueDistrict = x.CitizenIssueDistrict,
                      JariMiti = x.JariMiti,
                      GrandFatherName = x.GrandFatherName,
                      FatherName = x.FatherName,
                      StateId = x.StateId,
                      DistrictId = x.DistrictId,
                      PalikaId = x.PalikaId,
                      WardId = x.WardId,
                      OrganizationId = x.OrganizationId,
                      PPPhotoPath = x.PPPhotoPath,
                      CitizenPhotoPath = x.CitizenPhotoPath,
                  }).FirstOrDefaultAsync() ?? new BusconductorDetailViewModel();
        }
        public async Task<bool> InsertUpdateBusconductorDetail(BusconductorDetailViewModel model)
        {
            try
            {
                var PPPhotoo = "";
                var CitizenPhotoo = "";

                if (model.Id > 0)
                {
                    var data = await _context.BusconductorDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (model.PPFilePath != null)
                    {
                        PPPhotoo = _utility.UploadImgReturnPathAndName("BusconductorDetail", model.PPFilePath, "ProfilePhoto").Result.FilePath;
                        if (data.PPPhotoPath != null)
                        {
                            await _utility.RemoveFileFormServer(data.PPPhotoPath);
                        }
                    }
                    if (model.CitizenFilePath != null)
                    {
                        CitizenPhotoo = _utility.UploadImgReturnPathAndName("BusconductorDetail", model.CitizenFilePath, "CitizenPhoto").Result.FilePath;
                        if (data.CitizenPhotoPath != null)
                        {
                            await _utility.RemoveFileFormServer(data.CitizenPhotoPath);
                        }
                    }
                    if (data != null)
                    {
                        data.FullName = model.FullName;
                        data.FullNameNep = model.FullNameNep;
                        data.GenderId = model.GenderId;
                        data.MobileNo = model.MobileNo;
                        data.PanNo = model.PanNo;
                        data.CitizenNo = model.CitizenNo;
                        data.CitizenIssueDistrict = model.CitizenIssueDistrict;
                        data.JariMiti = model.JariMiti;
                        data.GrandFatherName = model.GrandFatherName;
                        data.FatherName = model.FatherName;
                        data.StateId = model.StateId;
                        data.DistrictId = model.DistrictId;
                        data.PalikaId = model.PalikaId;
                        data.WardId = model.WardId;
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
                        PPPhotoo = _utility.UploadImgReturnPathAndName("BusconductorDetail", model.PPFilePath, "ProfilePhoto").Result.FilePath;
                    }
                    if (model.CitizenFilePath != null)
                    {
                        CitizenPhotoo = _utility.UploadImgReturnPathAndName("BusconductorDetail", model.CitizenFilePath, "CitizenPhoto").Result.FilePath;
                    }
                    var orgid = await _utility.GetLoggedInOrgId();
                    if (orgid > 0)
                    {
                        var dataa = new BusconductorDetails()
                        {
                            OrganizationId = orgid,
                            FullName = model.FullName,
                            FullNameNep = model.FullNameNep,
                            GenderId = model.GenderId,
                            MobileNo = model.MobileNo,
                            PanNo = model.PanNo,
                            CitizenNo = model.CitizenNo,
                            CitizenIssueDistrict = model.CitizenIssueDistrict,
                            JariMiti = model.JariMiti,
                            GrandFatherName = model.GrandFatherName,
                            FatherName = model.FatherName,
                            StateId = model.StateId,
                            DistrictId = model.DistrictId,
                            PalikaId = model.PalikaId,
                            WardId = model.WardId,
                            PPPhotoPath = PPPhotoo,
                            CitizenPhotoPath = CitizenPhotoo,

                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.BusconductorDetails.AddAsync(dataa);
                        await _context.SaveChangesAsync();
                    }
                    else return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("BusconductorDetail Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        } 
        #endregion
    }
}
