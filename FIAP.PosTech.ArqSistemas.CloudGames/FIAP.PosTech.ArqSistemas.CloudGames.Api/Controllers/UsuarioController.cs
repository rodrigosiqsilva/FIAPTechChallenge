using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
