using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Client : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string ClientName { get; set; }

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
        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Nome do Animal")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string AnimalName { get; set; }

        [Required]
        [Display(Name = "Espécie")]
        public string Species { get; set; }

        [Required]
        [Display(Name = "Idade")]
        public string AnimalAge { get; set; }

        [Required]
        [Display(Name = "Raça")]
        public string Breed { get; set; }

        [MaxLength(250, ErrorMessage = "The field {0} can contain {1} characters length.")]
        [Display(Name = "Observações")]
        public string Note { get; set; }

                
        [Display(Name = "Imagem")]
        public string AnimalImageUrl { get; set; }

        public User User { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(AnimalImageUrl))
                {
                    return null;
                }

                return $"https://localhost:44309{AnimalImageUrl.Substring(1)}";
            }
        }
    }
}
