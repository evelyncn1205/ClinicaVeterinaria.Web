using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinariaWeb.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
