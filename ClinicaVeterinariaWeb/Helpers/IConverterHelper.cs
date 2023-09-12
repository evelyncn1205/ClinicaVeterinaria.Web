using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Models;

namespace ClinicaVeterinariaWeb.Helpers
{
    public interface IConverterHelper
    {
        Client ToClient(ClientViewModel model,string path, bool isNew);

        ClientViewModel ToClientViewModel(Client client);

        Employee ToEmployee (EmployeeViewModel model, string path, bool isNew);

        EmployeeViewModel ToEmployeeViewModel (Employee employee);

        Marcacao ToMarcacao (AddMarcacaoViewModel model, bool isNew);

        AddMarcacaoViewModel ToMarcacaoViewModel(Marcacao marcacao);
    }
}
