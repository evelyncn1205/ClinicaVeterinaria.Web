using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Client : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Client Name")]
        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }

        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }

        [Required]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Animal Name")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string AnimalName { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        [Display(Name = "Animal Age")]
        public string AnimalAge { get; set; }

        [Required]
        public string Breed { get; set; }

        [MaxLength(250, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Note { get; set; }

                
        [Display(Name = "Image")]
        public string AnimalImageUrl { get; set; }

        public User User { get; set; }
    }
}
