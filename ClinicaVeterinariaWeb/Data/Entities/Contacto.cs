using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinariaWeb.Data.Entities
{
    public class Contacto : IEntity
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mensagem { get; set; }


    }
}
