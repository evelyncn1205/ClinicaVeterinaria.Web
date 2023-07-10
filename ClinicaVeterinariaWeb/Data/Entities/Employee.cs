using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string LastName { get; set; }

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
        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Address { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Attendence Room")]
        public string Room { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public User User { get; set; }
    }
}
