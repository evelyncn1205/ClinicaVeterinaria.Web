using ClinicaVeterinariaWeb.Data.Entities;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public interface IConsultaRepository : IGenericRepository<Consulta>
    {
        public IQueryable GetAllWithUsers();
    }
}
