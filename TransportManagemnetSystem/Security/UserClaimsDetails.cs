using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using TransportManagemnetSystem.Data;

namespace TransportManagemnetSystem.Security
{
    public class UserClaimsDetails : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly TransportDbContext _context;

        private readonly string _userId;

        public UserClaimsDetails(UserManager<ApplicationUser> userManager
        , RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options, TransportDbContext context)
        : base(userManager, roleManager, options)
        {
            _context = context;

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Email", user.Email ?? ""));
            var orgdata = _context.OrganizationDetail.ToList();
            var orgIdFromOrgUser = _context.OrganizationUserDetails.Where(z => z.UserId == user.Id).Select(z => z.OrgId).FirstOrDefault();
            var orgNameFromOrgUser = _context.OrganizationDetail.Where(z => z.Id == orgIdFromOrgUser).Select(z => z.OrganizationName).FirstOrDefault();
            
            var OrgName = orgdata.Where(x => x.UserId == user.Id).Select(x => x.OrganizationName).FirstOrDefault();
            identity.AddClaim(new Claim("OrgName", OrgName ?? orgNameFromOrgUser ?? ""));
            var StateId = orgdata.Where(x => x.UserId == user.Id).Select(x => x.StateId).FirstOrDefault();
            identity.AddClaim(new Claim("StateId", StateId.ToString()));
            var DistrictId = orgdata.Where(x => x.UserId == user.Id).Select(x => x.DistrictId).FirstOrDefault();
            identity.AddClaim(new Claim("DistrictId", DistrictId.ToString()));
            var PalikaId = orgdata.Where(x => x.UserId == user.Id).Select(x => x.PalikaId).FirstOrDefault();
            identity.AddClaim(new Claim("PalikaId", PalikaId.ToString()));
            var WardNo = orgdata.Where(x => x.UserId == user.Id).Select(x => x.WardNo).FirstOrDefault();
            identity.AddClaim(new Claim("WardNo", WardNo.ToString()));
            var OrgId = orgdata.Where(x => x.UserId == user.Id).Select(x => x.Id).FirstOrDefault();
            identity.AddClaim(new Claim("OrgId", OrgId.ToString()));

            return identity;
        }
    }
}
