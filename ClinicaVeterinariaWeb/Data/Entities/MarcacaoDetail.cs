﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class MarcacaoDetail : IEntity
    {
        public int Id { get; set; }

        
        [Required]
        public Client Cliente { get; set; }

        [Required]
        [Display(Name = "Nome do Animal")]
        public string NomeAnimal { get; set; }

        [Required]
        [Display(Name = "Telemóvel")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Tipo de Consulta")]
        public string TipodaConsulta { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Hora")]
        public TimeSpan Hora { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }
    }
}
