using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Animal Name")]
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
        public string Note { get; set; }
        
    }
}
