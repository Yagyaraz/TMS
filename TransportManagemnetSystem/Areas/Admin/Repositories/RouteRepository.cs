using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Utilities;

namespace TransportManagemnetSystem.Areas.Admin.Repositories
{
    public class RouteRepository : IRoute
    {
        private readonly IUtility _utility;
        private readonly TransportDbContext _context;
        private readonly ILogger<RouteRepository> _logger;
        private readonly string _userId;
        private readonly IHttpContextAccessor _httpContext;
        public RouteRepository(TransportDbContext context, IHttpContextAccessor httpContext, ILogger<RouteRepository> logger, IUtility utility)
        {
            _context = context;
            _httpContext = httpContext;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger = logger;
            _utility = utility;
        }
        
        public async Task<List<RouteViewModel>> GetAllRoute()
        {
            return await _context.BusRoute
                .Select(x => new RouteViewModel
                {
                    Id = x.Id,
                    DestinationLatitude = x.DestinationLatitude,
                    DestinationLongnitude = x.DestinationLongnitude,
                    DestinationName = x.DestinationName,
                    DestinationTime = x.DestinationTime,
                    RouteName = x.RouteName,
                    SourceLatitude = x.SourceLatitude,
                    SourceLongnitude = x.SourceLongnitude,
                    SourceName = x.SourceName,
                    SourceTime = x.SourceTime,
                }).ToListAsync();
        }

        public async Task<RouteViewModel> GetRouteById(int id)
        {
            return await _context.BusRoute.Where(x => x.Id == id)
                 .Select(x => new RouteViewModel
                 {
                     Id = x.Id,
                     DestinationLatitude = x.DestinationLatitude,
                     DestinationLongnitude = x.DestinationLongnitude,
                     DestinationName = x.DestinationName,
                     DestinationTime = x.DestinationTime,
                     RouteName = x.RouteName,
                     SourceLatitude = x.SourceLatitude,
                     SourceLongnitude = x.SourceLongnitude,
                     SourceName = x.SourceName,
                     SourceTime = x.SourceTime,
                     StopSetupViewModelList = _context.StopSetup.Where(z => z.RouteId == id)
                     .Select(z => new StopSetupViewModel()
                     {
                         Id = z.Id,
                         RouteId = z.RouteId,
                         Latitude = z.Latitude,
                         Longnitude = z.Longnitude,
                         Name = z.Name,
                         StopTime = z.StopTime,
                         Time = z.Time,
                     }).ToList() ?? new List<StopSetupViewModel>()
                 }).FirstOrDefaultAsync() ?? new RouteViewModel();
        }

        public async Task<bool> InsertUpdateRoute(RouteViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (model.Id > 0)
                    {
                        var data = await _context.BusRoute.FirstOrDefaultAsync(x => x.Id == model.Id);
                        if (data != null)
                        {
                            data.DestinationLatitude = model.DestinationLatitude;
                            data.DestinationLongnitude = model.DestinationLongnitude;
                            data.DestinationName = model.DestinationName;
                            data.DestinationTime = model.DestinationTime;
                            data.RouteName = model.RouteName;
                            data.SourceLatitude = model.SourceLatitude;
                            data.SourceLongnitude = model.SourceLongnitude;
                            data.SourceName = model.SourceName;
                            data.SourceTime = model.SourceTime;

                            data.UpdatedBy = _userId;
                            data.UpdatedDate = DateTime.Now;

                            _context.Entry(data).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            if (model.StopSetupViewModelList != null)
                            {
                                foreach (var item in await _context.StopSetup.Where(x => x.RouteId == model.Id).ToListAsync())
                                {
                                    if (!model.StopSetupViewModelList.Any(x => x.Id == item.Id))
                                    {
                                        _context.StopSetup.Remove(item);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                foreach (var item in model.StopSetupViewModelList)
                                {
                                    var adata = await _context.StopSetup.FirstOrDefaultAsync(x => x.Id == item.Id);
                                    if (adata != null)
                                    {
                                        adata.Latitude = item.Latitude;
                                        adata.Longnitude = item.Longnitude;
                                        adata.Name = item.Name;
                                        adata.StopTime = item.StopTime;
                                        adata.Time = item.Time;

                                        _context.Entry(adata).State = EntityState.Modified;
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        var daat = new StopSetup()
                                        {
                                            RouteId = model.Id,
                                            Latitude = item.Latitude,
                                            Longnitude = item.Longnitude,
                                            Name = item.Name,
                                            StopTime = item.StopTime,
                                            Time = item.Time,
                                        };
                                        await _context.StopSetup.AddAsync(daat);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                        else return false;
                    }
                    else
                    {
                        var newdata = new BusRoute()
                        {
                            DestinationLatitude = model.DestinationLatitude,
                            DestinationLongnitude = model.DestinationLongnitude,
                            DestinationName = model.DestinationName,
                            DestinationTime = model.DestinationTime,
                            RouteName = model.RouteName,
                            SourceLatitude = model.SourceLatitude,
                            SourceLongnitude = model.SourceLongnitude,
                            SourceName = model.SourceName,
                            SourceTime = model.SourceTime,

                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.BusRoute.AddAsync(newdata);
                        await _context.SaveChangesAsync();
                        if (model.StopSetupViewModelList != null)
                        {
                            foreach (var item in model.StopSetupViewModelList)
                            {
                                var daat = new StopSetup()
                                {
                                    RouteId = newdata.Id,
                                    Latitude = item.Latitude,
                                    Longnitude = item.Longnitude,
                                    Name = item.Name,
                                    StopTime = item.StopTime,
                                    Time = item.Time,
                                };
                                await _context.StopSetup.AddAsync(daat);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Route Repo create/update with multiple table (stopsetup) Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }


        public async Task<List<OutRouteViewModel>> GetAllOutRoute()
        {
            return await _context.BusOutRoute
                .Select(x => new OutRouteViewModel
                {
                    Id = x.Id,
                    DestinationLatitude = x.DestinationLatitude,
                    DestinationLongnitude = x.DestinationLongnitude,
                    DestinationName = x.DestinationName,
                    DestinationTime = x.DestinationTime,
                    RouteName = x.RouteName,
                    SourceLatitude = x.SourceLatitude,
                    SourceLongnitude = x.SourceLongnitude,
                    SourceName = x.SourceName,
                    SourceTime = x.SourceTime,
                }).ToListAsync();
        }

        public async Task<OutRouteViewModel> GetOutRouteById(int id)
        {
            return await _context.BusOutRoute.Where(x => x.Id == id)
                 .Select(x => new OutRouteViewModel
                 {
                     Id = x.Id,
                     DestinationLatitude = x.DestinationLatitude,
                     DestinationLongnitude = x.DestinationLongnitude,
                     DestinationName = x.DestinationName,
                     DestinationTime = x.DestinationTime,
                     RouteName = x.RouteName,
                     SourceLatitude = x.SourceLatitude,
                     SourceLongnitude = x.SourceLongnitude,
                     SourceName = x.SourceName,
                     SourceTime = x.SourceTime,
                     StopSetupViewModelList = _context.OutStopSetup.Where(z => z.OutRouteId == id)
                     .Select(z => new StopSetupViewModel()
                     {
                         Id = z.Id,
                         RouteId = z.OutRouteId,
                         Latitude = z.Latitude,
                         Longnitude = z.Longnitude,
                         Name = z.Name,
                         StopTime = z.StopTime,
                         Time = z.Time,
                     }).ToList() ?? new List<StopSetupViewModel>()
                 }).FirstOrDefaultAsync() ?? new OutRouteViewModel();
        }

        public async Task<bool> InsertUpdateOutRoute(OutRouteViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (model.Id > 0)
                    {
                        var data = await _context.BusOutRoute.FirstOrDefaultAsync(x => x.Id == model.Id);
                        if (data != null)
                        {
                            data.DestinationLatitude = model.DestinationLatitude;
                            data.DestinationLongnitude = model.DestinationLongnitude;
                            data.DestinationName = model.DestinationName;
                            data.DestinationTime = model.DestinationTime;
                            data.RouteName = model.RouteName;
                            data.SourceLatitude = model.SourceLatitude;
                            data.SourceLongnitude = model.SourceLongnitude;
                            data.SourceName = model.SourceName;
                            data.SourceTime = model.SourceTime;

                            data.UpdatedBy = _userId;
                            data.UpdatedDate = DateTime.Now;

                            _context.Entry(data).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            if (model.StopSetupViewModelList != null)
                            {
                                foreach (var item in await _context.OutStopSetup.Where(x => x.OutRouteId == model.Id).ToListAsync())
                                {
                                    if (!model.StopSetupViewModelList.Any(x => x.Id == item.Id))
                                    {
                                        _context.OutStopSetup.Remove(item);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                foreach (var item in model.StopSetupViewModelList)
                                {
                                    var adata = await _context.OutStopSetup.FirstOrDefaultAsync(x => x.Id == item.Id);
                                    if (adata != null)
                                    {
                                        adata.Latitude = item.Latitude;
                                        adata.Longnitude = item.Longnitude;
                                        adata.Name = item.Name;
                                        adata.StopTime = item.StopTime;
                                        adata.Time = item.Time;

                                        _context.Entry(adata).State = EntityState.Modified;
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        var daat = new OutStopSetup()
                                        {
                                            OutRouteId = model.Id,
                                            Latitude = item.Latitude,
                                            Longnitude = item.Longnitude,
                                            Name = item.Name,
                                            StopTime = item.StopTime,
                                            Time = item.Time,
                                        };
                                        await _context.OutStopSetup.AddAsync(daat);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                        else return false;
                    }
                    else
                    {
                        var newdata = new BusOutRoute()
                        {
                            DestinationLatitude = model.DestinationLatitude,
                            DestinationLongnitude = model.DestinationLongnitude,
                            DestinationName = model.DestinationName,
                            DestinationTime = model.DestinationTime,
                            RouteName = model.RouteName,
                            SourceLatitude = model.SourceLatitude,
                            SourceLongnitude = model.SourceLongnitude,
                            SourceName = model.SourceName,
                            SourceTime = model.SourceTime,

                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.BusOutRoute.AddAsync(newdata);
                        await _context.SaveChangesAsync();
                        if (model.StopSetupViewModelList != null)
                        {
                            foreach (var item in model.StopSetupViewModelList)
                            {
                                var daat = new OutStopSetup()
                                {
                                    OutRouteId = newdata.Id,
                                    Latitude = item.Latitude,
                                    Longnitude = item.Longnitude,
                                    Name = item.Name,
                                    StopTime = item.StopTime,
                                    Time = item.Time,
                                };
                                await _context.OutStopSetup.AddAsync(daat);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("OutRoute Repo create/update with multiple table (Outstopsetup) Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
    }
}
