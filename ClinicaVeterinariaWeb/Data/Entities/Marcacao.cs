using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Marcacao : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Cliente { get; set; }

        [Required]
        [Display(Name ="Nome do Animal")]
        public string NomeAnimal { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Hora")]
        public TimeSpan Hora { get; set; }

        [Required]
        [Display(Name = "Tipo de Consulta")]
        public string TipodaConsulta { get; set; }

        [Required]
        [Display(Name = "Telemóvel")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public User User { get; set; }

        public IEnumerable<MarcacaoDetail> Items { get; set; }

        public double Quantity { get; set; }

        [Display(Name = "Data")]
        public DateTime? MarcacaoDateLocal => this.Data == null ? null : this.Data.ToLocalTime();
    }
}
