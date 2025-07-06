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
    public class VehicleDartaRepository : IVehicleDarta
    {
        private readonly IUtility _utility;
        private readonly TransportDbContext _context;
        private readonly ILogger<VehicleDartaRepository> _logger;
        private readonly string _userId;
        private readonly string _userStateId;
        private readonly IHttpContextAccessor _httpContext;
        public VehicleDartaRepository(TransportDbContext context, IHttpContextAccessor httpContext, ILogger<VehicleDartaRepository> logger, IUtility utility)
        {
            _context = context;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userStateId = httpContext.HttpContext.User.FindFirstValue("StateId");
            _logger = logger;
            _utility = utility;
        }

        public async Task<List<VehicleDartaViewModel>> GetAllVehicleDarta()
        {
            var orgid = await _utility.GetLoggedInOrgId();
            var query = _context.VehicleDarta.AsQueryable();

            if (_httpContext.HttpContext.User.IsInRole(UserRoles.Admin))
            {
                query = query.Where(x => x.OrganizationDetail.StateId == int.Parse(_userStateId));
            }
            else if (_httpContext.HttpContext.User.IsInRole(UserRoles.SubAdmin))
            {
                query = query.Where(x => orgid == 0 || x.OrganizationId == orgid);
            }

            return await query.Select(x => new VehicleDartaViewModel
            {
                Id = x.Id,
                EngineTypeId = x.EngineTypeId,
                BuildYear = x.BuildYear,
                ChasisNo = x.ChasisNo,
                EngineNo = x.EngineNo,
                ManufactureCompanyId = x.ManufactureCompanyId,
                StandardTypeId = x.StandardTypeId,
                TotalSeat = x.TotalSeat,
                VehicleCategoryId = x.VehicleCategoryId,
                VehicleModel = x.VehicleModel,
                VehicleNumber = x.VehicleNumber,
                VehicleTypeId = x.VehicleTypeId,
                Cilinder = x.Cilinder,
                HorsePower = x.HorsePower,
                VehicleColor = x.VehicleColor,
                VehicleMirrorColor = x.VehicleMirrorColor,
                SancharUpakaran = x.SancharUpakaran,
                TotalWeight = x.TotalWeight,
                KarPramanPatra = x.KarPramanPatra,
                Jachpass = x.Jachpass,
                PradushanPramanpatra = x.PradushanPramanpatra,
                RouteName = x.RouteName,
                OtherPraman = x.OtherPraman,
                DartaPramanPartaFile = x.DartaPramanPartaFile,
                ParichayePatraFile = x.ParichayePatraFile,
                LicenseFile = x.LicenseFile,
                ChasisLength = x.ChasisLength,
                IsApproved = x.IsApproved,
                IsAssgned = x.IsAssgned,
                VehicleOwnerName = x.VehicleOwnerName,
                MobileNo = x.MobileNo,
            }).ToListAsync();
        }

        public async Task<VehicleDartaViewModel> GetVehicleDarta(int id)
        {
            return await _context.VehicleDarta.Where(x => x.Id == id)
                 .Select(x => new VehicleDartaViewModel()
                 {
                     Id = x.Id,
                     EngineTypeId = x.EngineTypeId,
                     BuildYear = x.BuildYear,
                     ChasisNo = x.ChasisNo,
                     EngineNo = x.EngineNo,
                     ManufactureCompanyId = x.ManufactureCompanyId,
                     StandardTypeId = x.StandardTypeId,
                     TotalSeat = x.TotalSeat,
                     VehicleCategoryId = x.VehicleCategoryId,
                     VehicleModel = x.VehicleModel,
                     VehicleNumber = x.VehicleNumber,
                     VehicleTypeId = x.VehicleTypeId,
                     Cilinder = x.Cilinder,
                     HorsePower = x.HorsePower,
                     VehicleColor = x.VehicleColor,
                     VehicleMirrorColor = x.VehicleMirrorColor,
                     SancharUpakaran = x.SancharUpakaran,
                     TotalWeight = x.TotalWeight,
                     KarPramanPatra = x.KarPramanPatra,
                     Jachpass = x.Jachpass,
                     PradushanPramanpatra = x.PradushanPramanpatra,
                     RouteName = x.RouteName,
                     OtherPraman = x.OtherPraman,
                     DartaPramanPartaFile = x.DartaPramanPartaFile,
                     ParichayePatraFile = x.ParichayePatraFile,
                     LicenseFile = x.LicenseFile,
                     ChasisLength = x.ChasisLength,
                     VehicleOwnerName = x.VehicleOwnerName,
                     MobileNo = x.MobileNo,
                     bluebookRouteBimaViewModelList =  _context.bluebookRouteBima.Where(z => z.VehicleId == x.Id)
                     .Select(z => new bluebookRouteBimaViewModel()
                     {
                         Id = z.Id,
                         VehicleId = z.VehicleId,
                         BimaCompanyName = z.BimaCompanyName,
                         BimaDartaDate = z.BimaDartaDate,
                         BimaDartaMiti = z.BimaDartaMiti,
                         BimaEndMiti = z.BimaEndMiti,
                         BimaEndDate = z.BimaEndDate,
                         BluebookDartaDate = z.BluebookDartaDate,
                         BluebookDartaMiti = z.BluebookDartaMiti,
                         BluebookEndMiti = z.BluebookEndMiti,
                         BluebookEndDate = z.BluebookEndDate,
                         PollutionDartaDate = z.PollutionDartaDate,
                         PollutionDartaMiti = z.PollutionDartaMiti,
                         PollutionEndMiti = z.PollutionEndMiti,
                         PollutionEndDate = z.PollutionEndDate,
                         RouteDartaDate = z.RouteDartaDate,
                         RouteDartaMiti = z.RouteDartaMiti,
                         RouteEndMiti = z.RouteEndMiti,
                         RouteEndDate = z.RouteEndDate,
                     }).FirstOrDefault() ?? new bluebookRouteBimaViewModel(),
                 }).FirstOrDefaultAsync() ?? new VehicleDartaViewModel();
        }
        

        public async Task<VehicleDartaFilesViewModel> VehicleDartaFiles(int id)
        {
            return await _context.VehicleDarta.Where(x => x.Id == id)
                 .Select(x => new VehicleDartaFilesViewModel()
                 {
                     VehicleDartaId = x.Id,
                     VehicleNumber = x.VehicleNumber,
                     DartaPramanPartaFile = x.DartaPramanPartaFile,
                     ParichayePatraFile = x.ParichayePatraFile,
                     LicenseFile = x.LicenseFile,
                     AdditionalFiles = _context.VehicleDartaAddImage.Where(z => z.VehicleDartaId == x.Id)
                     .Select(z => new VehicleDartaAddImageViewModel()
                     {
                         Id = z.Id,
                         FilePath = z.FilePath,
                         FileName = z.FileName,
                     }).ToList() ?? new List<VehicleDartaAddImageViewModel>(),
                 }).FirstOrDefaultAsync() ?? new VehicleDartaFilesViewModel();
        }


        public async Task<bool> InsertUpdateVehicleDarta(VehicleDartaViewModel model)
        {
            using (var transection = _context.Database.BeginTransaction())
            {
                try
                {
                    var DartaPramanPartaPhoto = "";
                    var ParichayePatraPhoto = "";
                    var LicensePhoto = "";
                    if (model.Id > 0)
                    {
                        var data = await _context.VehicleDarta.FirstOrDefaultAsync(x => x.Id == model.Id);
                        if (model.IDartaPramanPartaFile != null)
                        {
                            DartaPramanPartaPhoto = _utility.UploadImgReturnPathAndName("VehicleDarta", model.IDartaPramanPartaFile, "DartaPramanPatra").Result.FilePath;
                            if (data.DartaPramanPartaFile != null)
                            {
                                await _utility.RemoveFileFormServer(data.DartaPramanPartaFile);
                            }
                        }
                        if (model.IParichayePatraFile != null)
                        {
                            ParichayePatraPhoto = _utility.UploadImgReturnPathAndName("VehicleDarta", model.IParichayePatraFile, "ParichayePatra").Result.FilePath;
                            if (data.ParichayePatraFile != null)
                            {
                                await _utility.RemoveFileFormServer(data.ParichayePatraFile);
                            }
                        }
                        if (model.ILicenseFile != null)
                        {
                            LicensePhoto = _utility.UploadImgReturnPathAndName("VehicleDarta", model.ILicenseFile, "License").Result.FilePath;
                            if (data.LicenseFile != null)
                            {
                                await _utility.RemoveFileFormServer(data.LicenseFile);
                            }
                        }
                        if (data != null)
                        {
                            data.VehicleOwnerName = model.VehicleOwnerName;
                            data.MobileNo = model.MobileNo;
                            data.EngineTypeId = model.EngineTypeId;
                            data.BuildYear = model.BuildYear;
                            data.ChasisNo = model.ChasisNo;
                            data.EngineNo = model.EngineNo;
                            data.ManufactureCompanyId = model.ManufactureCompanyId;
                            data.StandardTypeId = model.StandardTypeId;
                            data.TotalSeat = model.TotalSeat;
                            data.VehicleCategoryId = model.VehicleCategoryId;
                            data.VehicleModel = model.VehicleModel;
                            data.VehicleNumber = model.VehicleNumber;
                            data.VehicleTypeId = model.VehicleTypeId;
                            data.Cilinder = model.Cilinder;
                            data.HorsePower = model.HorsePower;
                            data.VehicleColor = model.VehicleColor;
                            data.VehicleMirrorColor = model.VehicleMirrorColor;
                            data.SancharUpakaran = model.SancharUpakaran;
                            data.TotalWeight = model.TotalWeight;
                            data.KarPramanPatra = model.KarPramanPatra;
                            data.Jachpass = model.Jachpass;
                            data.PradushanPramanpatra = model.PradushanPramanpatra;
                            data.RouteName = model.RouteName;
                            data.OtherPraman = model.OtherPraman;
                            data.ChasisLength = model.ChasisLength;
                            data.DartaPramanPartaFile = DartaPramanPartaPhoto == "" ? data.DartaPramanPartaFile : DartaPramanPartaPhoto;
                            data.ParichayePatraFile = ParichayePatraPhoto == "" ? data.ParichayePatraFile : ParichayePatraPhoto;
                            data.LicenseFile = LicensePhoto == "" ? data.LicenseFile : LicensePhoto;
                            data.UpdatedBy = _userId;
                            data.UpdatedDate = DateTime.Now;

                            _context.Entry(data).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            var dat = await _context.bluebookRouteBima.Where(x => x.Id == model.bluebookRouteBimaViewModelList.Id).FirstOrDefaultAsync(); 
                            if (dat !=null)
                            {
                                dat.BluebookDartaMiti = model.bluebookRouteBimaViewModelList.BluebookDartaMiti;
                                dat.BluebookDartaDate = model.bluebookRouteBimaViewModelList.BluebookDartaDate;
                                dat.BluebookEndMiti = model.bluebookRouteBimaViewModelList.BluebookEndMiti;
                                dat.BluebookEndDate = model.bluebookRouteBimaViewModelList.BluebookEndDate;
                                dat.PollutionDartaMiti = model.bluebookRouteBimaViewModelList.PollutionDartaMiti;
                                dat.PollutionDartaDate = model.bluebookRouteBimaViewModelList.PollutionDartaDate;
                                dat.PollutionEndMiti = model.bluebookRouteBimaViewModelList.PollutionEndMiti;
                                dat.PollutionEndDate = model.bluebookRouteBimaViewModelList.PollutionEndDate;
                                dat.RouteDartaMiti = model.bluebookRouteBimaViewModelList.RouteDartaMiti;
                                dat.RouteDartaDate = model.bluebookRouteBimaViewModelList.RouteDartaDate;
                                dat.RouteEndMiti = model.bluebookRouteBimaViewModelList.RouteEndMiti;
                                dat.RouteEndDate = model.bluebookRouteBimaViewModelList.RouteEndDate;
                                dat.BimaCompanyName = model.bluebookRouteBimaViewModelList.BimaCompanyName;
                                dat.BimaDartaMiti = model.bluebookRouteBimaViewModelList.BimaDartaMiti;
                                dat.BimaDartaDate = model.bluebookRouteBimaViewModelList.BimaDartaDate;
                                dat.BimaEndMiti = model.bluebookRouteBimaViewModelList.BimaEndMiti;
                                dat.BimaEndDate = model.bluebookRouteBimaViewModelList.BimaEndDate;

                                dat.UpdatedBy = _userId;
                                dat.UpdatedDate = DateTime.Now;
                                _context.Entry(dat).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                var info = new bluebookRouteBima()
                                {
                                    VehicleId = data.Id,
                                    BluebookDartaMiti = model.bluebookRouteBimaViewModelList.BluebookDartaMiti,
                                    BluebookDartaDate = model.bluebookRouteBimaViewModelList.BluebookDartaDate,
                                    BluebookEndMiti = model.bluebookRouteBimaViewModelList.BluebookEndMiti,
                                    BluebookEndDate = model.bluebookRouteBimaViewModelList.BluebookEndDate,
                                    PollutionDartaMiti = model.bluebookRouteBimaViewModelList.PollutionDartaMiti,
                                    PollutionDartaDate = model.bluebookRouteBimaViewModelList.PollutionDartaDate,
                                    PollutionEndMiti = model.bluebookRouteBimaViewModelList.PollutionEndMiti,
                                    PollutionEndDate = model.bluebookRouteBimaViewModelList.PollutionEndDate,
                                    RouteDartaMiti = model.bluebookRouteBimaViewModelList.RouteDartaMiti,
                                    RouteDartaDate = model.bluebookRouteBimaViewModelList.RouteDartaDate,
                                    RouteEndMiti = model.bluebookRouteBimaViewModelList.RouteEndMiti,
                                    RouteEndDate = model.bluebookRouteBimaViewModelList.RouteEndDate,
                                    BimaCompanyName = model.bluebookRouteBimaViewModelList.BimaCompanyName,
                                    BimaDartaMiti = model.bluebookRouteBimaViewModelList.BimaDartaMiti,
                                    BimaDartaDate = model.bluebookRouteBimaViewModelList.BimaDartaDate,
                                    BimaEndMiti = model.bluebookRouteBimaViewModelList.BimaDartaMiti,
                                    BimaEndDate = model.bluebookRouteBimaViewModelList.BimaDartaDate,

                                    CreatedBy = _userId,
                                    CreatedDate = DateTime.Now,
                                };
                                await _context.bluebookRouteBima.AddAsync(info);
                                await _context.SaveChangesAsync();
                            }
                        }
                        else return false;
                    }
                    else
                    {
                        if (model.IDartaPramanPartaFile != null)
                        {
                            DartaPramanPartaPhoto = _utility.UploadImgReturnPathAndName("VehicleDarta", model.IDartaPramanPartaFile, "DartaPramanPatra").Result.FilePath;
                        }
                        if (model.IParichayePatraFile != null)
                        {
                            ParichayePatraPhoto = _utility.UploadImgReturnPathAndName("VehicleDarta", model.IParichayePatraFile, "ParichayePatra").Result.FilePath;
                        }
                        if (model.ILicenseFile != null)
                        {
                            LicensePhoto = _utility.UploadImgReturnPathAndName("VehicleDarta", model.ILicenseFile, "License").Result.FilePath;
                        }
                        var orgid = await _utility.GetLoggedInOrgId();
                        if (orgid > 0)
                        {
                            var VehicleDarta = new VehicleDarta()
                            {
                                VehicleOwnerName = model.VehicleOwnerName,
                                MobileNo = model.MobileNo,
                                EngineTypeId = model.EngineTypeId,
                                BuildYear = model.BuildYear,
                                ChasisNo = model.ChasisNo,
                                EngineNo = model.EngineNo,
                                ManufactureCompanyId = model.ManufactureCompanyId,
                                StandardTypeId = model.StandardTypeId,
                                TotalSeat = model.TotalSeat,
                                VehicleCategoryId = model.VehicleCategoryId,
                                VehicleModel = model.VehicleModel,
                                VehicleNumber = model.VehicleNumber,
                                VehicleTypeId = model.VehicleTypeId,
                                OrganizationId = orgid,
                                Cilinder = model.Cilinder,
                                HorsePower = model.HorsePower,
                                VehicleColor = model.VehicleColor,
                                VehicleMirrorColor = model.VehicleMirrorColor,
                                SancharUpakaran = model.SancharUpakaran,
                                TotalWeight = model.TotalWeight,
                                KarPramanPatra = model.KarPramanPatra,
                                Jachpass = model.Jachpass,
                                ChasisLength = model.ChasisLength,
                                PradushanPramanpatra = model.PradushanPramanpatra,
                                RouteName = model.RouteName,
                                OtherPraman = model.OtherPraman,
                                DartaPramanPartaFile = DartaPramanPartaPhoto,
                                ParichayePatraFile = ParichayePatraPhoto,
                                LicenseFile = LicensePhoto,

                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.VehicleDarta.AddAsync(VehicleDarta);
                            await _context.SaveChangesAsync();

                            var info = new bluebookRouteBima()
                            {
                                VehicleId = VehicleDarta.Id,
                                BluebookDartaMiti = model.bluebookRouteBimaViewModelList.BluebookDartaMiti,
                                BluebookDartaDate = model.bluebookRouteBimaViewModelList.BluebookDartaDate,
                                BluebookEndMiti = model.bluebookRouteBimaViewModelList.BluebookEndMiti,
                                BluebookEndDate = model.bluebookRouteBimaViewModelList.BluebookEndDate,
                                PollutionDartaMiti = model.bluebookRouteBimaViewModelList.PollutionDartaMiti,
                                PollutionDartaDate = model.bluebookRouteBimaViewModelList.PollutionDartaDate,
                                PollutionEndMiti = model.bluebookRouteBimaViewModelList.PollutionEndMiti,
                                PollutionEndDate = model.bluebookRouteBimaViewModelList.PollutionEndDate,
                                RouteDartaMiti = model.bluebookRouteBimaViewModelList.RouteDartaMiti,
                                RouteDartaDate = model.bluebookRouteBimaViewModelList.RouteDartaDate,
                                RouteEndMiti = model.bluebookRouteBimaViewModelList.RouteEndMiti,
                                RouteEndDate = model.bluebookRouteBimaViewModelList.RouteEndDate,
                                BimaCompanyName = model.bluebookRouteBimaViewModelList.BimaCompanyName,
                                BimaDartaMiti = model.bluebookRouteBimaViewModelList.BimaDartaMiti,
                                BimaDartaDate = model.bluebookRouteBimaViewModelList.BimaDartaDate,
                                BimaEndMiti = model.bluebookRouteBimaViewModelList.BimaDartaMiti,
                                BimaEndDate = model.bluebookRouteBimaViewModelList.BimaDartaDate,
                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.bluebookRouteBima.AddAsync(info);
                            await _context.SaveChangesAsync();
                        }
                        else return false;
                    }
                    await transection.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("VehicleCategory Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                    await transection.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> InserApproveData(int Id, string Name, string Darja, string Date, string approveNo)
        {
            using (var transection = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = await _context.VehicleDarta.FirstOrDefaultAsync(x => x.Id == Id);
                    if (data != null)
                    {
                        data.IsApproved = true;
                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        var newdata = new VehicleDartaApprove
                        {
                            Name = Name,
                            Date = Date,
                            Darja = Darja,
                            ApproveNo = approveNo,
                            VehicheDatraId = Id,

                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.VehicleDartaApprove.AddAsync(newdata);
                        await _context.SaveChangesAsync();
                    }
                    await transection.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Vehicle approve Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                    await transection.RollbackAsync();
                    return false;
                }
            }
        }
        
        public async Task<bool> InsertAssignData(int Id, int DriverId, int ConductorId, int RouteId)
        {
            using (var transection = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = await _context.VehicleDarta.FirstOrDefaultAsync(x => x.Id == Id);
                    if (data != null)
                    {
                        data.IsAssgned = true;
                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        var newdata = new VehicleAssignedData
                        {
                            VehicleId = Id,
                            DriverId = DriverId,
                            ConductorId = ConductorId,
                            RouteId = RouteId,

                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.VehicleAssignedData.AddAsync(newdata);
                        await _context.SaveChangesAsync();
                    }
                    await transection.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("VehicleAssignedData approve Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                    await transection.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> InsertFiles(VehicleDartaAddImageViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //var aa = await _context.VehicleDartaAddImage.Where(x => x.VehicleDartaId == model.VehicleDartaId).ToListAsync();
                    //foreach (var item in aa)
                    //{
                    //    _context.Remove(item);
                    //    _context.SaveChanges();
                    //}
                    //foreach (var file in model.Files)
                    //{
                    //    var path = _utility.UploadImgReturnPathAndName("VehicleDarta", file, "AdditionalFile").Result.FilePath;

                    //    var files = new VehicleDartaAddImage()
                    //    {
                    //        VehicleDartaId = model.VehicleDartaId,
                    //        FilePath = path,
                    //    };
                    //    await _context.VehicleDartaAddImage.AddAsync(files);
                    //    await _context.SaveChangesAsync();
                    //}
                    if (model.FileList != null)
                    {
                        foreach (var item in await _context.VehicleDartaAddImage.Where(x => x.VehicleDartaId == model.VehicleDartaId).ToListAsync())
                        {
                            if (!model.FileList.Any(x => x.Id == item.Id))
                            {
                                await _utility.RemoveFileFormServer(item.FilePath);
                                _context.VehicleDartaAddImage.Remove(item);
                                await _context.SaveChangesAsync();
                            }
                        }

                        // Loop through all files in the model
                        foreach (var file in model.FileList)
                        {
                            //if (file.Files != null) // New or updated file
                            //{
                                var path = _utility.UploadImgReturnPathAndName("VehicleDarta", file.Files, "AdditionalFile").Result.FilePath;

                                if (file.Id == 0)
                                {
                                    var newFile = new VehicleDartaAddImage
                                    {
                                        VehicleDartaId = model.VehicleDartaId,
                                        FilePath = path,
                                        FileName = file.FileName,
                                    };
                                    await _context.VehicleDartaAddImage.AddAsync(newFile);
                                }
                                else 
                                {
                                    var existingFile = await _context.VehicleDartaAddImage.FindAsync(file.Id);
                                    if (existingFile != null)
                                    {
                                        existingFile.FilePath = file.FilePath != null ? path : _context.VehicleDartaAddImage.Where(x => x.Id == file.Id).Select(x => x.FilePath).FirstOrDefault();
                                        existingFile.FileName = file.FileName;
                                    }
                                }
                            //}
                        }
                    }
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("VehicleDartaAddImage Repo add files Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
        public async Task<List<VehicleDartaAddImageViewModel>> GetFileById(int? Vehicleid)
        {
            var data = await _context.VehicleDartaAddImage.Where(x => x.VehicleDartaId == Vehicleid)
                .Select(x => new VehicleDartaAddImageViewModel()
                {
                    Id = x.Id,
                    VehicleDartaId = x.VehicleDartaId,
                    FilePath = x.FilePath,
                    FileName =x.FileName,
                }).ToListAsync();
            return data;
        }

        public async Task<List<Seat>> BookingSeat(int Vehicleid)
        {
            var vehicle = await _context.VehicleDarta.FirstOrDefaultAsync(v => v.Id == Vehicleid);
            if (vehicle == null)
            {
                return new List<Seat>();
            }
            var bookedSeats = await _context.BookedSeat
                .Where(b => b.VehicleDartaId == Vehicleid && b.IsBooked)
                .Select(b => b.SeatNumber).ToListAsync();

            var seats = Enumerable
                .Range(1, vehicle.TotalSeat)
                .Select(seatNumber => new Seat
                {
                    SeatNumber = seatNumber.ToString(),
                    IsBooked = bookedSeats.Contains(seatNumber)
                }).ToList();
            return seats;
        }
        public async Task<bool> BookSeatNow(BookingRequest request)
        {
            try
            {
                foreach (var seat in request.SeatNumbers)
                {
                    var data = new BookedSeat()
                    {
                        SeatNumber = seat,
                        IsBooked = true,
                        VehicleDartaId = request.Vehicleid,
                        BookedBy = _userId,
                        BookedDateEn = DateTime.Now,
                    };
                    await _context.BookedSeat.AddAsync(data);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<MyBookedSeatViewModel>> GetAllBookedSeats()
        {
           return await _context.BookedSeat.Where(x => x.BookedBy == _userId)
                .Select(x => new MyBookedSeatViewModel()
                {
                    SeatNo = x.SeatNumber,
                    VehicleNumb = _context.VehicleDarta.Where(z => z.Id == x.VehicleDartaId).Select(z => z.VehicleNumber).FirstOrDefault(),
                }).ToListAsync();
        }
    }
}
