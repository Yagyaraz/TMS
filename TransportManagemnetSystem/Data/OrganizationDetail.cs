using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Data
{
    public class OrganizationDetail
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationNameEng { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int PalikaId { get; set; }
        public int WardNo { get; set; }
        public string Area { get; set; }
        public string Email { get; set; }
        public string DartaMiti { get; set; }
        public DateTime? DartaMitiEng { get; set; }
        public string DartaLocation { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
