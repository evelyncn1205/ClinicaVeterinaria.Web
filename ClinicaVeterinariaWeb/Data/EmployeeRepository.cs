using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Employees.Include(e => e.User);
        }
    }
}
