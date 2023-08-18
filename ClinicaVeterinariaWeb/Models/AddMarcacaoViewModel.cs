using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinariaWeb.Models
{
    public class AddMarcacaoViewModel
    {
        [Display(Name="Cliente")]
        [Range(1, int.MaxValue, ErrorMessage = "Você deve selecionar um cliente")]
        public int ClienteId { get; set; }


        public IEnumerable<SelectListItem> Cliente { get; set; }

        [Display(Name = "Nome do Animal")]
        public string AnimalName { get; set; }

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

        public double Quantity { get; set; }
    }
}
