using ClinicaVeterinariaWeb.Data.Entities;

namespace ClinicaVeterinariaWeb.Data
{
    public class ConsultaRepositorycs : GenericRepository<Consulta>, IConsultaRepository
    {
        private readonly DataContext _context;
        public ConsultaRepositorycs(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
