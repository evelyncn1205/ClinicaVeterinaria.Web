using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
