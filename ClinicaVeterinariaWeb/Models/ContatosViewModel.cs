using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Models
{
    public class ContatosViewModel : Contacto
    {
        [Display(Name = "Clientes")]
        public int ClienteId { get; set; }

        public IEnumerable<SelectListItem> Clientes { get; set; }

        public string Phone { get; set; }
    }
}
