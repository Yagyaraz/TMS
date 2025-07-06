using Microsoft.AspNetCore.Mvc.Rendering;
using TransportManagemnetSystem.Models;

namespace TransportManagemnetSystem.Utilities
{
    public interface IUtility
    {
        Task<SelectList> GetAllFiscalYearList();
        Task<SelectList> GetSelectListRoles();
        Task<string> GetProjectName();

        Task<SelectList> GetUserList();
        Task<SelectList> GetOrganizationSelectListItems();
        Task<SelectList> GetStateSelectListItems();
        Task<SelectList> GetDistrictSelectListItems(int? stateId);
        Task<SelectList> GetPalikaSelectListItems(int? distId);
        Task<SelectList> GetGenderSelectListItems();
        Task<SelectList> GetLicenseSelectListItems();

        Task<int> GetLoggedInOrgId();
        Task<SelectList> GetAllVehicleCategoryList();
        Task<SelectList> GetAllVehicleTypeList(int? vehiclecatid);
        Task<SelectList> GetAllStandardTypeList();
        Task<SelectList> GetAllEngineTypeList();
        Task<SelectList> GetAllManufactureCompanyList();
        Task<FileUploadModel> UploadImgReturnPathAndName(string folderName, IFormFile img, string name);
        Task RemoveFileFormServer(string path);

        Task<List<PiechartViewModel>> GetPieChartDataOrg();
        Task<List<PiechartAppDispViewModel>> GetPieChartDataApprovUnApprov();
        Task<List<ColumnChartViewModel>> GetColumnChart();
        Task<List<PieChartForRolesViewModel>> GetPieChartForRoles();
        Task<List<LineChartViewModel>> GetVehicleChartData();
        Task<List<PiechartViewModel>> GetRouteLineChart();

        Task<SelectList> GetAllVehicleList();
        Task<SelectList> GetAllDriverList();
        Task<SelectList> GetAllConductorList();
        Task<SelectList> GetAllRouteList();

        Task<List<PiechartViewModel>> GetPieDataNumberOne();
        Task<List<PiechartViewModel>> GetPieDataNumberTwo();
        Task<List<VehicleEngineTypeChartViewModel>> GetLineDataNumberThree();
        Task<List<BusRouteStopsViewModel>> GetLineDataNumberFour();
        Task<bool> SendSms(int id, string text);

    }
}
