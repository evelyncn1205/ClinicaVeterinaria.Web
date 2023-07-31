using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Apelido")]
        public string LastName { get; set; }
    }
}
