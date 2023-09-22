using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Models
{
    public class CancelarViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        [Range(1, int.MaxValue, ErrorMessage = "Você deve selecionar um cliente")]
        public int ClienteId { get; set; }

        public IEnumerable<SelectListItem> Cliente { get; set; }
    }
}
