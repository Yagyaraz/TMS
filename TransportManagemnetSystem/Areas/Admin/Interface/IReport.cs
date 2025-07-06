using TransportManagemnetSystem.Areas.Admin.Models;

namespace TransportManagemnetSystem.Areas.Admin.Interface
{
    public interface IReport
    {
        Task<List<OrginizationReportViewModel>> GetAllOrganixzation();
        Task<OrginizationReportViewModel> GetAllOrganixzationById(int id);

        Task<RouteSearchModel> RouteSearch(string source, string destination);
        Task<RouteViewModel> RouteDetailsById(int id);
        Task<RouteSearchModel> OutRouteSearch(string source, string destination);
        Task<OutRouteViewModel> OutRouteDetailsById(int id);
        Task<List<VehicleDartaViewModel>> VehicleDartaList();
        Task<VehicleDartaReportViewModel> VehicleDartaDetailsById(int id);
        Task<List<DriverDetailReportViewModel>> DriverList();
        Task<DriverDetailReportViewModel> DriverDetailReportById(int id);
        Task<List<BusconductorDetailReportViewModel>> CoDriverList();
        Task<BusconductorDetailReportViewModel> CoDriverDetailReportById(int id);

    }
}
