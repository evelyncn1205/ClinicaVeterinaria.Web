using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
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
        public string Address { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Attendence Room")]
        public string Room { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }


    }
}
