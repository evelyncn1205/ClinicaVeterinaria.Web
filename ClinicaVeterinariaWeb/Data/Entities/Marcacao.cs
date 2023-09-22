using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Marcacao : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name ")]
        public User User { get; set; }

        [Required]
        public string Cliente { get; set; }

        [Required]
        [Display(Name ="Nome do Animal")]
        public string NomeAnimal { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Hora")]       
        public TimeSpan Hora { get; set; }

        [Display(Name = "Status ")]
        public StatusConsulta? StatusConsulta { get; set; } 

        [Required]
        [Display(Name = "Tipo de Consulta")]
        public string TipodaConsulta { get; set; }

        [Required]
        [Display(Name = "Telemóvel")]
        public string CellPhone { get; set; }

        
        [Display(Name = "Email")]
        public string Email { get; set; }               

        public IEnumerable<MarcacaoDetail> Items { get; set; }

        public double Quantity { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? MarcacaoDateLocal => this.Data == null ? null : this.Data.ToLocalTime();
    }
}
