using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "नाम")]
        public string Name { get; set; }
        public string OfficerName { get; set; }
        public string Post { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        public string Address { get; set; }




        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Ward")]
        public int? WardId { get; set; }
        [Display(Name = "ई-मेल")]
        public string UserName { get; set; }
        [Display(Name = "रोल")]
        public string Role { get; set; }
        [Display(Name = "रोल")]
        public string RoleId { get; set; }
        [Display(Name = "वडा")]
        public string WardName { get; set; }
        [Display(Name = "अवस्था")]
        public bool Active { get; set; }
    }

    public class OrgUserViewModel
    {
        public int Id { get; set; }
        public int OrgId { get; set; }

        [Display(Name = "नाम")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Phone Number")]
        public string MobileNo { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }
        [Display(Name = "ई-मेल")]
        public string UserName { get; set; }
        [Display(Name = "रोल")]
        public string Role { get; set; }
        [Display(Name = "रोल")]
        public string RoleId { get; set; }
        [Display(Name = "अवस्था")]
        public bool Active { get; set; }

        public string UserId { get; set; }
    }

    public class UserLogViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
