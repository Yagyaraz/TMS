namespace TransportManagemnetSystem.Areas.Admin.Models
{
    
    public class Booking
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<int> SelectedSeats { get; set; } = new List<int>();
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
