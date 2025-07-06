using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class BusRoute
    {
        [Key]
        public int Id { get; set; }
        public string RouteName { get; set; }
        public string SourceName { get; set; }
        public TimeSpan SourceTime { get; set; }
        public string SourceLatitude { get; set; }
        public string SourceLongnitude { get; set; }
        public string DestinationName { get; set; }
        public TimeSpan DestinationTime { get; set; }
        public string DestinationLatitude { get; set; }
        public string DestinationLongnitude { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class StopSetup
    {
        [Key]
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan StopTime { get; set; }
        public string Latitude { get; set; }
        public string Longnitude { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [ForeignKey(nameof(RouteId))]
        public BusRoute BusRoute { get; set; }
    }
    public class BusOutRoute
    {
        [Key]
        public int Id { get; set; }
        public string RouteName { get; set; }
        public string SourceName { get; set; }
        public TimeSpan SourceTime { get; set; }
        public string SourceLatitude { get; set; }
        public string SourceLongnitude { get; set; }
        public string DestinationName { get; set; }
        public TimeSpan DestinationTime { get; set; }
        public string DestinationLatitude { get; set; }
        public string DestinationLongnitude { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class OutStopSetup
    {
        [Key]
        public int Id { get; set; }
        public int OutRouteId { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan StopTime { get; set; }
        public string Latitude { get; set; }
        public string Longnitude { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [ForeignKey(nameof(OutRouteId))]
        public BusOutRoute BusOutRoute { get; set; }
    }
}
