using System.ComponentModel.DataAnnotations;

namespace TransportManagemnetSystem.Models
{
    public class SmsViewModel
    {
        [Phone]
        [Display(Name = "To Number")]
        public string To { get; set; }

        //[Required(ErrorMessage = "From Number Required", AllowEmptyStrings = false)]
        [Phone]
        [Display(Name = "From Number")]
        public string from { get; set; }

        //[Required(ErrorMessage = "Message Text Required", AllowEmptyStrings = false)]
        [Display(Name = "Message Text")]
        public string Text { get; set; }
        public string token { get; set; }
    }
    public class ConfigureSmsModel
    {
        public string Token { get; set; }
        public string From { get; set; }
    }
}
