using ClinicaVeterinariaWeb.Data.Entities;

namespace ClinicaVeterinariaWeb.Data
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext _context;
        public ClientRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
