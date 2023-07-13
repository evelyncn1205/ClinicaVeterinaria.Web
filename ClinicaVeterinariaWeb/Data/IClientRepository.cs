using ClinicaVeterinariaWeb.Data.Entities;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();

    }
}
