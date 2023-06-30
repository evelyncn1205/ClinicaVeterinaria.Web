using ClinicaVeterinariaWeb.Data.Entities;

namespace ClinicaVeterinariaWeb.Data
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
