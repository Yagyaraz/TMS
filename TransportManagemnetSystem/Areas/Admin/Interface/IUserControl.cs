using TransportManagemnetSystem.Areas.Admin.Models;
using TransportManagemnetSystem.Models;

namespace TransportManagemnetSystem.Areas.Admin.Interface
{
    public interface IUserControl
    {
        Task<List<UserViewModel>> GetUserList();
        Task<List<OrgUserViewModel>> GetOrgUserList();
        Task<OrganizationDetailViewModel> UserDetailsById(string id);
        Task<UserViewModel> GetUserById(string id);
        Task<bool> ResetUserPassword(string id);
        Task<int> UserActivateOrDeactivate(string id);
        Task<bool> AssignOrChangeRole(string id, string role);

        Task<List<UserLogViewModel>> GetAllUserDetail(string userid);
    }
}
