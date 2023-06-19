﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [Display(Name = "Client Name")]
        [Required]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }

        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }

        [Required]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Animal Name")]
        public string AnimalName { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        [Display(Name = "Animal Age")]
        public string AnimalAge { get; set; }

        [Required]
        public string Breed { get; set; }

        public string Note { get; set; }

        
        [Display(Name = "Image")]
        public string AnimalImageUrl { get; set; }
    }
}
