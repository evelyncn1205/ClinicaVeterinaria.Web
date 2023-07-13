using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public class ConsultaRepositorycs : GenericRepository<Consulta>, IConsultaRepository
    {
        private readonly DataContext _context;
        public ConsultaRepositorycs(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.Consulta.Include(c => c.User);
        }
    }
}
