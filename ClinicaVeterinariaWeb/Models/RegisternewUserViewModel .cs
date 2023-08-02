using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Models
{
    public class RegisternewUserViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apelido")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Perfil de usuário : ")]
        [UIHint("List")]
        public List<SelectListItem> Roles { get; set; }

        public string Role { get; set; }

        public RegisternewUserViewModel()
        {
            Roles = new List<SelectListItem>();
            Roles.Add(new SelectListItem() { Value = "1", Text = "Admin" });
            Roles.Add(new SelectListItem() { Value = "2", Text = "Employee" });
            Roles.Add(new SelectListItem() { Value = "3", Text = "Client" });
            
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
