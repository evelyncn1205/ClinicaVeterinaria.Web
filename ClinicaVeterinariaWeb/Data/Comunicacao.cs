using ClinicaVeterinariaWeb.Data.Entities;

namespace ClinicaVeterinariaWeb.Data
{
    public class Comunicacao : IEntity
    {
        public int Id { get; set ; }

        public string Name { get; set ; }

        public string Email { get; set ; }

        public string Mensagem { get; set ; }
    }
}
