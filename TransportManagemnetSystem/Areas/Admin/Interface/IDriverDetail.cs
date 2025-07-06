using TransportManagemnetSystem.Areas.Admin.Models;

namespace TransportManagemnetSystem.Areas.Admin.Interface
{
    public interface IDriverDetail
    {
        #region Driver
        Task<List<DriverDetailViewModel>> GetAllDriverDetail();
        Task<DriverDetailViewModel> GetDriverDetailById(int id);
        Task<bool> InsertUpdateDriverDetail(DriverDetailViewModel model); 
        #endregion
        #region Busconductor
        Task<List<BusconductorDetailViewModel>> GetAllBusconductorDetail();
        Task<BusconductorDetailViewModel> GetBusconductorDetailById(int id);
        Task<bool> InsertUpdateBusconductorDetail(BusconductorDetailViewModel model); 
        #endregion
    }
}
