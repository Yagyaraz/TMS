using NuGet.Protocol.Core.Types;

namespace TransportManagemnetSystem.Models
{
    public class PiechartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class DashboardViewModel
    {
        public List<TotalCountViewModel> TotalList { get; set; } = new List<TotalCountViewModel>();
        public List<TotalAccordingToOrgViewModel> TotalListAccToOrg { get; set; } = new List<TotalAccordingToOrgViewModel>();
    }
    public class TotalCountViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class TotalAccordingToOrgViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class PiechartAppDispViewModel
    {
        public int Id { get; set; }
        public bool? Isapproved { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }

    }
    public class ColumnChartViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int count { get; set; }
    }
    public class PieChartForRolesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int count { get; set; }
    }
    public class LineChartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int count { get; set; }
    }
    public class VehicleEngineTypeChartViewModel
    {
        public string Category { get; set; }
        public Dictionary<string, int> EngineTypeCounts { get; set; } 
    }
    public class BusRouteStopsViewModel
    {
        public string RouteName { get; set; }
        public List<string> Stops { get; set; }
    }
}
