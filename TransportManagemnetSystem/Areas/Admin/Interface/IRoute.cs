using TransportManagemnetSystem.Areas.Admin.Models;

namespace TransportManagemnetSystem.Areas.Admin.Interface
{
    public interface IRoute
    {
        Task<List<RouteViewModel>> GetAllRoute();
        Task<RouteViewModel> GetRouteById(int id);
        Task<bool> InsertUpdateRoute(RouteViewModel model);


        Task<List<OutRouteViewModel>> GetAllOutRoute();
        Task<OutRouteViewModel> GetOutRouteById(int id);
        Task<bool> InsertUpdateOutRoute(OutRouteViewModel model);
    }
}
