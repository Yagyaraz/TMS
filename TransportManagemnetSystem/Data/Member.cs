using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace TransportManagemnetSystem.Data
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string FullName { get; set; }
        public string FullNameEng { get; set; }
        public string CitizenNo { get; set; }
        public string CitizenIssueDate { get; set; }
        public int? CitizenIssueDistricrId { get; set; }
        public string RegNo { get; set; }
        public string PanVatNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string PPPhotoPath { get; set; }
        public string CitizenPhotoPath { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public OrganizationDetail OrganizationDetail { get; set; }
    }
}
