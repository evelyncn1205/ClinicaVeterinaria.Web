using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class MarcacaoDetail : IEntity
    {
        public int Id { get; set; }

        
        [Required]
        public Client Client { get; set; }

        [Required]
        [Display(Name = "Nome do Animal")]
        public string NomeAnimal { get; set; }

        [Required]
        [Display(Name = "Telemóvel")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "Tipo de Consulta")]
        public string TipodaConsulta { get; set; }

        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Hora")]
        public TimeSpan Hora { get; set; }

       
        public int Quantity { get; internal set; }
    }
}
