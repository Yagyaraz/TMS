namespace TransportManagemnetSystem.Data
{
    public class UserLog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
