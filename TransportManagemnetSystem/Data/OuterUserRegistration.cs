using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class OuterUserRegistration
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FullNameEng { get; set; }
        public string CitizenNo { get; set; }
        public string CitizenIssueMiti { get; set; }
        public DateTime? CitizenIssueMitiEng { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int PalikaId { get; set; }
        public int WardNo { get; set; }
        public string Area { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public OrganizationDetail OrganizationDetail { get; set; }

    }
}
