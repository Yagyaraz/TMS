using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string CitizenshipNo { get; set; }

    }
}
