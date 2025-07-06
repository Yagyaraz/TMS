using TransportManagemnetSystem.Areas.Admin.Models;

namespace TransportManagemnetSystem.Areas.Admin.Interface
{
    public interface ICommon
    {
        #region FiscalYear
        Task<List<FiscalYearViewModel>> GetAllFiscalYear();
        Task<FiscalYearViewModel> GetFiscalYear(int id);
        Task<bool> InsertUpdateFiscalYear(FiscalYearViewModel model);
        #endregion
        #region Office
        Task<List<OfficeViewModel>> GetAllOffice();
        Task<OfficeViewModel> GetOffice(int id);
        Task<bool> InsertUpdateOffice(OfficeViewModel model);
        #endregion
        #region VehicleCategory
        Task<List<VehicleCategoryViewModel>> GetAllVehicleCategory();
        Task<VehicleCategoryViewModel> GetVehicleCategory(int id);
        Task<bool> InsertUpdateVehicleCategory(VehicleCategoryViewModel model);
        #endregion
        #region EngineType
        Task<List<EngineTypeViewModel>> GetAllEngineType();
        Task<EngineTypeViewModel> GetEngineType(int id);
        Task<bool> InsertUpdateEngineType(EngineTypeViewModel model);
        #endregion
        #region ManufactureCompany
        Task<List<ManufactureCompanyViewModel>> GetAllManufactureCompany();
        Task<ManufactureCompanyViewModel> GetManufactureCompany(int id);
        Task<bool> InsertUpdateManufactureCompany(ManufactureCompanyViewModel model);
        #endregion
        #region StandardType
        Task<List<StandardTypeViewModel>> GetAllStandardType();
        Task<StandardTypeViewModel> GetStandardType(int id);
        Task<bool> InsertUpdateStandardType(StandardTypeViewModel model);
        #endregion
        #region VehicleType
        Task<List<VehicleTypeViewModel>> GetAllVehicleType();
        Task<VehicleTypeViewModel> GetVehicleType(int id);
        Task<bool> InsertUpdateVehicleType(VehicleTypeViewModel model);
        #endregion
        #region FAQ
        Task<List<FAQViewModel>>GetALlFAQ();
        Task<FAQViewModel> GetFAQById(int id);
        Task<bool> InsertUpdateFAQ(FAQViewModel model);
        Task<bool> DeleteFAQ(int id);
        #endregion
    }
}
