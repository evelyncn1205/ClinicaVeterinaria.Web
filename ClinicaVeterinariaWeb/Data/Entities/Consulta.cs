using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Consulta : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [MaxLength(100, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Nome do Animal")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string AnimalName { get; set; }

        [Required]
        [Display(Name = "Telemóvel")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Hora")]
        public TimeSpan Time { get; set; }

        [Required]
        [Display(Name = "Veterinário")]
        public string Doctor { get; set; }

        [Required]
        [Display(Name = "Tipo de Consulta")]
        public string ConsultationType { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "The field {0} can contain {1} characters length.")]
        [Display(Name = "Observações")]
        public string Note { get; set; }

        public User User { get; set; }

    }
}
