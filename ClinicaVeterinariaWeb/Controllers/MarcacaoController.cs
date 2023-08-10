using ClinicaVeterinariaWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Controllers
{
    [Authorize]
    public class MarcacaoController : Controller
    {
        private readonly iMarcacaoRepository _marcacaoRepository;
        public MarcacaoController(iMarcacaoRepository marcacaoRepository)
        {
            _marcacaoRepository = marcacaoRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _marcacaoRepository.GetMarcacaoAsync(this.User.Identity.Name);

            return View(model);
        }


    }
}
