using TransportManagemnetSystem.Models;

namespace TransportManagemnetSystem.Utilities
{
    public interface IDashboard
    {
        Task<DashboardViewModel> GetDashBoardData();
    }
}
