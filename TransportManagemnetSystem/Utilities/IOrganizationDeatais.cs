using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Models;

namespace TransportManagemnetSystem.Utilities
{
    public interface IOrganizationDeatais
    {
        Task<bool> InsertUpdateOrganizationDeatais(OrganizationDetailViewModel model);
        Task<bool> InsertUpdateOuterUserDetail(OuterUserRegistrationViewModel model);
        Task<bool> InsertUpdateOrgUserDeatails(OrgUserViewModel model);
    }
}
