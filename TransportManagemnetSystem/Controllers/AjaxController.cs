using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransportManagemnetSystem.Utilities;

namespace TransportManagemnetSystem.Controllers
{
    [AllowAnonymous]
    public class AjaxController : Controller
    {
        private readonly IUtility _utility = null;
        public AjaxController(IUtility utility)
        {
            _utility = utility;
        }
        public async Task<JsonResult> GetDistrictByStateId(int id)
        {
            return Json(await _utility.GetDistrictSelectListItems(id));
        }
        public async Task<JsonResult> GetPalikaByDistId(int id)
        {
            return Json(await _utility.GetPalikaSelectListItems(id));
        }        
        public async Task<JsonResult> GetVehileTypeByCatId(int id)
        {
            return Json(await _utility.GetAllVehicleTypeList(id));
        }
        public async Task<JsonResult> GetPieChartDataOrg()
        {
            return Json(await _utility.GetPieChartDataOrg());
        }
        public async Task<JsonResult> GetPieChartDataApprovUnApprov()
        {
            return Json(await _utility.GetPieChartDataApprovUnApprov());
        }
        public async Task<JsonResult> GetColumnChart()
        {
            return Json(await _utility.GetColumnChart());
        }
        public async Task<JsonResult> GetPieChartForRoles()
        {
            return Json(await _utility.GetPieChartForRoles());
        }
        public async Task<JsonResult> GetLineChartVehicle()
        {
            return Json(await _utility.GetVehicleChartData());
        }
        public async Task<JsonResult> GetRouteLineChart()
        {
            return Json(await _utility.GetRouteLineChart());
        }

        ///org datas///

        public async Task<JsonResult> GetPieDataNumberOne()
        {
            return Json(await _utility.GetPieDataNumberOne());
        }
        public async Task<JsonResult> GetPieDataNumberTwo()
        {
            return Json(await _utility.GetPieDataNumberTwo());
        }
        public async Task<JsonResult> GetLineDataNumberThree()
        {
            return Json(await _utility.GetLineDataNumberThree());
        }
        public async Task<JsonResult> GetLineDataNumberFour()
        {
            return Json(await _utility.GetLineDataNumberFour());
        }
    }
}
