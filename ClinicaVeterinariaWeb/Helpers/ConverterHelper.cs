using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Models;
using System.IO;

namespace ClinicaVeterinariaWeb.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Client ToClient(ClientViewModel model, string path,bool isNew)
        {
            return new Client
            {
                Id = isNew ? 0 : model.Id,
                AnimalImageUrl = path,
                FullName = model.FullName,
                Document=model.Document,
                AnimalAge= model.AnimalAge,
                AnimalName = model.AnimalName,
                CellPhone = model.CellPhone,
                FixedPhone = model.FixedPhone,
                Email = model.Email,
                Address = model.Address,
                Species = model.Species,
                Breed = model.Breed,
                Note = model.Note,
                User = model.User,
            };
        }

        public ClientViewModel ToClientViewModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                AnimalImageUrl = client.AnimalImageUrl,
                FullName = client.FullName,
                Document=client.Document,
                AnimalAge= client.AnimalAge,
                AnimalName = client.AnimalName,
                CellPhone = client.CellPhone,
                FixedPhone = client.FixedPhone,
                Email = client.Email,
                Address = client.Address,
                Species = client.Species,
                Breed = client.Breed,
                Note = client.Note,
                User = client.User,
            };
        }

        public Employee ToEmployee(EmployeeViewModel model,string path, bool isNew)
        {
            return new Employee
            {
                Id= isNew ? 0 : model.Id,
                ImageUrl = path,
                FullName=model.FullName,                
                Address=model.Address,
                Email=model.Email,
                CellPhone=model.CellPhone,
                FixedPhone=model.FixedPhone,
                Role=model.Role,
                Room=model.Room,
                Document=model.Document,
                User=model.User,

            };
        }

        public EmployeeViewModel ToEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id=employee.Id,
                ImageUrl = employee.ImageUrl,
                FullName=employee.FullName,               
                Address=employee.Address,
                Email=employee.Email,
                CellPhone=employee.CellPhone,
                FixedPhone=employee.FixedPhone,
                Role=employee.Role,
                Room=employee.Room,
                Document=employee.Document,
                User=employee.User,
            };
        }


        
    }
}
