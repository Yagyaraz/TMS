using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class OrganizationUserDetails
    {
        [Key]
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }


        [ForeignKey(nameof(OrgId))]
        public OrganizationDetail OrgDetail { get; set; }
    }
}
