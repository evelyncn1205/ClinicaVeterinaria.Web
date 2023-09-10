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

        [Display(Name = "Morada")]
        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string Address { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(20, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Cidade")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a city")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "País")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a county")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

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
