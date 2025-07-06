using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;

namespace TransportManagemnetSystem.Areas.Admin.Repositories
{
    public class CommonRepository: ICommon
    {
        private readonly TransportDbContext _context;
        private readonly ILogger<CommonRepository> _logger;
        private readonly string _userId;
        private readonly IHttpContextAccessor _httpContext;
        public CommonRepository(TransportDbContext context, IHttpContextAccessor httpContext, ILogger<CommonRepository> logger)
        {
            _context = context;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger = logger;
        }

        #region FiscalYear
        public async Task<List<FiscalYearViewModel>> GetAllFiscalYear()
        {
            return await _context.FiscalYear
                .Select(x => new FiscalYearViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Name_En = x.Name_En,
                    Code = x.Code,
                    StartYear = x.StartYear,
                    EndYear = x.EndYear,
                    DisplayPosition = x.DisplayPosition,
                    IsActive = x.IsActive,
                    DateFrom = x.DateFrom,
                    DateFromEng = x.DateFromEng,
                    DateTo = x.DateTo,
                    DateToEng = x.DateToEng,
                    PreviousFiscalYearId = x.PreviousFiscalYearId,
                    PreviousFiscalYearName = (x.PreviousFiscalYearId == null) ? null : x.PreviousFiscalYear.Name
                }).ToListAsync();
        }
        public async Task<FiscalYearViewModel> GetFiscalYear(int id)
        {
            return await _context.FiscalYear.Where(x => x.Id == id)
                .Select(x => new FiscalYearViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Name_En = x.Name_En,
                    Code = x.Code,
                    StartYear = x.StartYear,
                    EndYear = x.EndYear,
                    DisplayPosition = x.DisplayPosition,
                    IsActive = x.IsActive,
                    DateFrom = x.DateFrom,
                    DateFromEng = x.DateFromEng,
                    DateTo = x.DateTo,
                    DateToEng = x.DateToEng,
                    PreviousFiscalYearId = x.PreviousFiscalYearId,
                }).FirstOrDefaultAsync() ?? new FiscalYearViewModel();
        }
        public async Task<bool> InsertUpdateFiscalYear(FiscalYearViewModel model)
        {
            try
            {
                if (model.IsActive == true)
                {
                    foreach (var item in await _context.FiscalYear.Where(x => x.IsActive == true).ToListAsync())
                    {
                        item.IsActive = false;
                        _context.Entry(item).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }

                var fiscalYear = await _context.FiscalYear.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (fiscalYear != null)
                {
                    fiscalYear.Name = model.Name;
                    fiscalYear.Name_En = model.Name_En;
                    fiscalYear.Code = model.Code;
                    fiscalYear.StartYear = model.StartYear;
                    fiscalYear.EndYear = model.EndYear;
                    fiscalYear.DisplayPosition = model.DisplayPosition;
                    fiscalYear.IsActive = model.IsActive;
                    fiscalYear.DateFrom = model.DateFrom;
                    fiscalYear.DateFromEng = model.DateFromEng;
                    fiscalYear.DateTo = model.DateTo;
                    fiscalYear.DateToEng = model.DateToEng;
                    fiscalYear.PreviousFiscalYearId = model.PreviousFiscalYearId;
                    fiscalYear.UpdatedBy = _userId;
                    fiscalYear.UpdatedDate = DateTime.Now;

                    _context.Entry(fiscalYear).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var fiscal = new FiscalYear()
                    {
                        Name = model.Name,
                        Name_En = model.Name_En,
                        Code = model.Code,
                        StartYear = model.StartYear,
                        EndYear = model.EndYear,
                        DisplayPosition = model.DisplayPosition,
                        IsActive = model.IsActive,
                        DateFrom = model.DateFrom,
                        DateFromEng = model.DateFromEng,
                        DateTo = model.DateTo,
                        DateToEng = model.DateToEng,
                        PreviousFiscalYearId = model.PreviousFiscalYearId,
                        CreatedBy = _userId,
                        CreatedDate = DateTime.Now,
                    };
                    await _context.FiscalYear.AddAsync(fiscal);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("FiscalYear Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        #endregion
        #region Office
        public async Task<List<OfficeViewModel>> GetAllOffice()
        {
            return await _context.Office
                .Select(x => new OfficeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Address = x.Address,
                    Email = x.Email,
                    FaxNo = x.FaxNo,
                    PalikaId = x.PalikaId,
                    PhoneNo = x.PhoneNo,
                    Url = x.Url,
                    StateId = x.StateId,
                    DistrictId = x.DistrictId,
                    StateName = x.State.StateNameNep,
                    DistrictName = x.District.DistrictNameNep,
                    PalikaName = x.Palika.PalikaNameNep,
                }).ToListAsync();
        }
        public async Task<OfficeViewModel> GetOffice(int id)
        {
            return await _context.Office//.Where(x=> x.Id == id)
                .Select(x => new OfficeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Address = x.Address,
                    Email = x.Email,
                    FaxNo = x.FaxNo,
                    PalikaId = x.PalikaId,
                    PhoneNo = x.PhoneNo,
                    Url = x.Url,
                    StateId = x.StateId,
                    DistrictId = x.DistrictId,
                    StateName = x.State.StateNameNep,
                    DistrictName = x.District.DistrictNameNep,
                    PalikaName = x.Palika.PalikaNameNep,
                }).FirstOrDefaultAsync() ?? new OfficeViewModel();
        }
        public async Task<bool> InsertUpdateOffice(OfficeViewModel model)
        {
            try
            {
                var office = await _context.Office.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (office != null)
                {
                    office.Name = model.Name;
                    office.Code = model.Code;
                    office.Address = model.Address;
                    office.Email = model.Email;
                    office.StateId = model.StateId;
                    office.DistrictId = model.DistrictId;
                    office.PalikaId = model.PalikaId;
                    office.PhoneNo = model.PhoneNo;
                    office.FaxNo = model.FaxNo;
                    office.Url = model.Url;
                    office.UpdatedBy = _userId;
                    office.UpdatedDate = DateTime.Now;

                    _context.Entry(office).State = EntityState.Modified;
                }
                else
                {
                    office = new Office()
                    {
                        Name = model.Name,
                        Code = model.Code,
                        Address = model.Address,
                        Email = model.Email,
                        PhoneNo = model.PhoneNo,
                        FaxNo = model.FaxNo,
                        Url = model.Url,
                        StateId = model.StateId,
                        DistrictId = model.DistrictId,
                        PalikaId = model.PalikaId,
                        CreatedBy = _userId,
                        CreatedDate = DateTime.Now,
                    };
                    await _context.Office.AddAsync(office);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Office Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        #endregion
        #region VehicleCategory
        public async Task<List<VehicleCategoryViewModel>> GetAllVehicleCategory()
        {
            return await _context.VehicleCategory
                .Select(x => new VehicleCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).ToListAsync();
        }
        public async Task<VehicleCategoryViewModel> GetVehicleCategory(int id)
        {
            return await _context.VehicleCategory.Where(x => x.Id == id)
                .Select(x => new VehicleCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).FirstOrDefaultAsync() ?? new VehicleCategoryViewModel();
        }
        public async Task<bool> InsertUpdateVehicleCategory(VehicleCategoryViewModel model)
        {
            try
            {
                var VehicleCategory = await _context.VehicleCategory.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (VehicleCategory != null)
                {
                    VehicleCategory.Name = model.Name;
                    VehicleCategory.NameNep = model.NameNep;

                    _context.Entry(VehicleCategory).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var fiscal = new VehicleCategory()
                    {
                        Name = model.Name,
                        NameNep = model.NameNep,
                    };
                    await _context.VehicleCategory.AddAsync(fiscal);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("VehicleCategory Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        #endregion
        #region EngineType
        public async Task<List<EngineTypeViewModel>> GetAllEngineType()
        {
            return await _context.EngineType
                .Select(x => new EngineTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).ToListAsync();
        }
        public async Task<EngineTypeViewModel> GetEngineType(int id)
        {
            return await _context.EngineType.Where(x => x.Id == id)
                .Select(x => new EngineTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).FirstOrDefaultAsync() ?? new EngineTypeViewModel();
        }
        public async Task<bool> InsertUpdateEngineType(EngineTypeViewModel model)
        {
            try
            {
                var EngineType = await _context.EngineType.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (EngineType != null)
                {
                    EngineType.Name = model.Name;
                    EngineType.NameNep = model.NameNep;

                    _context.Entry(EngineType).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var fiscal = new EngineType()
                    {
                        Name = model.Name,
                        NameNep = model.NameNep,
                    };
                    await _context.EngineType.AddAsync(fiscal);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("EngineType Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        #endregion
        #region ManufactureCompany
        public async Task<List<ManufactureCompanyViewModel>> GetAllManufactureCompany()
        {
            return await _context.ManufactureCompany
                .Select(x => new ManufactureCompanyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).ToListAsync();
        }
        public async Task<ManufactureCompanyViewModel> GetManufactureCompany(int id)
        {
            return await _context.ManufactureCompany.Where(x => x.Id == id)
                .Select(x => new ManufactureCompanyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).FirstOrDefaultAsync() ?? new ManufactureCompanyViewModel();
        }
        public async Task<bool> InsertUpdateManufactureCompany(ManufactureCompanyViewModel model)
        {
            try
            {
                var ManufactureCompany = await _context.ManufactureCompany.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (ManufactureCompany != null)
                {
                    ManufactureCompany.Name = model.Name;
                    ManufactureCompany.NameNep = model.NameNep;

                    _context.Entry(ManufactureCompany).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var fiscal = new ManufactureCompany()
                    {
                        Name = model.Name,
                        NameNep = model.NameNep,
                    };
                    await _context.ManufactureCompany.AddAsync(fiscal);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ManufactureCompany Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        #endregion
        #region StandardType
        public async Task<List<StandardTypeViewModel>> GetAllStandardType()
        {
            return await _context.StandardType
                .Select(x => new StandardTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).ToListAsync();
        }
        public async Task<StandardTypeViewModel> GetStandardType(int id)
        {
            return await _context.StandardType.Where(x => x.Id == id)
                .Select(x => new StandardTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                }).FirstOrDefaultAsync() ?? new StandardTypeViewModel();
        }
        public async Task<bool> InsertUpdateStandardType(StandardTypeViewModel model)
        {
            try
            {
                var StandardType = await _context.StandardType.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (StandardType != null)
                {
                    StandardType.Name = model.Name;
                    StandardType.NameNep = model.NameNep;

                    _context.Entry(StandardType).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var fiscal = new StandardType()
                    {
                        Name = model.Name,
                        NameNep = model.NameNep,
                    };
                    await _context.StandardType.AddAsync(fiscal);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("StandardType Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        #endregion
        #region VehicleType
        public async Task<List<VehicleTypeViewModel>> GetAllVehicleType()
        {
            return await _context.VehicleType
                .Select(x => new VehicleTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                    VehicleCategoryId = x.VehicleCategoryId,
                }).ToListAsync();
        }
        public async Task<VehicleTypeViewModel> GetVehicleType(int id)
        {
            return await _context.VehicleType.Where(x => x.Id == id)
                .Select(x => new VehicleTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameNep = x.NameNep,
                    VehicleCategoryId = x.VehicleCategoryId,
                }).FirstOrDefaultAsync() ?? new VehicleTypeViewModel();
        }
        public async Task<bool> InsertUpdateVehicleType(VehicleTypeViewModel model)
        {
            try
            {
                var VehicleType = await _context.VehicleType.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (VehicleType != null)
                {
                    VehicleType.Name = model.Name;
                    VehicleType.NameNep = model.NameNep;
                    VehicleType.VehicleCategoryId = model.VehicleCategoryId;

                    _context.Entry(VehicleType).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var fiscal = new VehicleType()
                    {
                        Name = model.Name,
                        NameNep = model.NameNep,
                        VehicleCategoryId = model.VehicleCategoryId,    
                    };
                    await _context.VehicleType.AddAsync(fiscal);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("VehicleType Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }


        #endregion
        #region FAQ
        public async Task<List<FAQViewModel>> GetALlFAQ()
        {
            return await _context.FAQ
                .Select(x => new FAQViewModel()
                {
                    Id = x.Id,
                    Question = x.Question,
                    Answer = x.Answer,                   
                }).ToListAsync()??new List<FAQViewModel>();
        }

        public async Task<FAQViewModel> GetFAQById(int id)
        {
            return await _context.FAQ.Where(x => x.Id == id).Select(x => new FAQViewModel()
            {
                Id = x.Id,
                Question = x.Question,
                Answer = x.Answer,
            }).FirstOrDefaultAsync() ?? new FAQViewModel();
        }

        public async Task<bool> InsertUpdateFAQ(FAQViewModel model)
        {
            try
            {
                var data = await _context.FAQ.FindAsync(model.Id);
                if (data != null)
                {
                    data.Answer = model.Answer;
                    data.Question = model.Question;
                    _context.Entry(data).State = EntityState.Modified;
                }
                else
                {
                    var faq = new FAQ()
                    {
                        Answer = model.Answer,
                        Question = model.Question,
                        Status = true
                    };
                    await _context.AddAsync(faq);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
                {
                _logger.LogInformation("FAQ Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;

            }
        }
        public async Task<bool> DeleteFAQ(int id)
        {
            try
            {
                var data = await _context.FAQ.FindAsync(id);
                if (data != null)
                {
                    data.Status = false;
                    _context.Entry(data).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }

                _logger.LogWarning($"FAQ with ID {id} not found.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting FAQ with ID {id}.");
                return false;
            }
        }
        #endregion
    }
}
