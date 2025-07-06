using TransportManagemnetSystem.Areas.Admin.Models;

namespace TransportManagemnetSystem.Areas.Admin.Interface
{
    public interface IMember
    {
        Task<List<MemberViewModel>> GetAllMember();
        Task<MemberViewModel> GetMemberById(int id);
        Task<bool> InsertUpdateMember(MemberViewModel model);
    }
}
