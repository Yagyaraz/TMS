using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Data;

namespace TransportManagemnetSystem.Areas.Admin.Interface
{
    public interface IVehicleDarta
    {
        Task<List<VehicleDartaViewModel>> GetAllVehicleDarta();
        Task<VehicleDartaViewModel> GetVehicleDarta(int id);
        Task<VehicleDartaFilesViewModel> VehicleDartaFiles(int id);
        Task<bool> InsertUpdateVehicleDarta(VehicleDartaViewModel model);

        Task<bool> InserApproveData(int Id, string Name, string Darja, string Date, string approveNo);
        Task<bool> InsertAssignData(int Id, int DriverId, int ConductorId, int RouteId);


        Task<List<VehicleDartaAddImageViewModel>> GetFileById(int? Vehicleid);
        Task<bool> InsertFiles(VehicleDartaAddImageViewModel model);

        Task<List<Seat>> BookingSeat(int Vehicleid);
        Task<bool> BookSeatNow(BookingRequest request);

        Task<List<MyBookedSeatViewModel>> GetAllBookedSeats();
    }
}
