using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        [Display(Name = "Nome")]
        public string FullName { get; set; }

        

        [Required]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Display(Name = "Telefone Fixo")]
        public string FixedPhone { get; set; }

        [Required]
        [Display(Name = "Telemóvel")]
        public string CellPhone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters length.")]
        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Cargo")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Sala de Atendimento")]
        public string Room { get; set; }

        [Display(Name = "Imagem")]
        public string ImageUrl { get; set; }

        public User User { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }

                return $"https://localhost:44309{ImageUrl.Substring(1)}";
            }
        }
    }
}
