using ClinicaVeterinariaWeb.Data.Entities;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IQueryable GetAllWithUsers();
    }
}
