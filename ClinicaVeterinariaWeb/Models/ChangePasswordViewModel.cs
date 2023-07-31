using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Password atual")]
        public string OldPassword { get; set; }


        [Required]
        [Display(Name = "Nova password")]
        public string NewPassword { get; set; }


        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
