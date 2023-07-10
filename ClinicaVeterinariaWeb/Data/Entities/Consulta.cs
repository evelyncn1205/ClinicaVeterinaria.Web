using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Consulta : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Client Name")]
        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Animal Name")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string AnimalName { get; set; }

        [Required]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Required]

        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public string Doctor { get; set; }

        [Required]
        [Display(Name = "Consultation Type")]
        public string ConsultationType { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Note { get; set; }

        public User User { get; set; }

    }
}
