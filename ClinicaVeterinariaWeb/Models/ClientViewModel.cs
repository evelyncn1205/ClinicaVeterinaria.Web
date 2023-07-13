using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Models
{
    public class ClientViewModel : Client
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
