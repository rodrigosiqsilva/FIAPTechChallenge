using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Services;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Enums;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController(IUsuarioService usuarioService, ICorrelationIdGenerator correlationIdGenerator,
        BaseLogger<UsuarioController> logger) :
        ControllerBase
    {

        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly ICorrelationIdGenerator _correlationIdGenerator = correlationIdGenerator;
        private readonly BaseLogger<UsuarioController> _logger = logger;

        [HttpPost("IncluirAsync")]
        public IActionResult Incluir([FromServices] ICorrelationIdGenerator _correlationIdGenerator, [FromBody] Usuario usuario)
        {

            if (usuario == null)
            {
                return BadRequest("Objeto usuário não enviado no body do request.");
            }

            return Accepted(usuario);
        }
        //public IActionResult Incluir([FromServices] ICorrelationIdGenerator _correlationIdGenerator, [FromBody] Usuario usuario,
        //    [FromServices] IValidator<Usuario> validator)
        //{

        //    if (usuario == null)
        //    {
        //        return BadRequest("Objeto usuário não enviado no body do request.");
        //    }

        //    var validationResult = validator.Validate(usuario);

        //    if (!validationResult.IsValid)
        //    {
        //        // Retorna os erros formatados
        //        return BadRequest(validationResult.Errors);
        //    }

        //    return Accepted(usuario);
        //}

        [HttpPut("AtualizarAsync")]
        public async Task<IActionResult> AtualizarAsync([FromServices] ICorrelationIdGenerator _correlationIdGenerator, [FromBody] Usuario usuario)
        {
            return NoContent(); // Retorna 204 (sucesso sem conteúdo)
        }

        [HttpDelete("ExcluirAsync")]
        public async Task<IActionResult> ExcluirAsync([FromServices] ICorrelationIdGenerator _correlationIdGenerator, int id)
        {
            return NoContent(); // Retorna 204 (sucesso sem conteúdo)
        }

        [HttpGet("BuscarPorIdAsync/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> BuscarPorIdAsync([FromServices] ICorrelationIdGenerator _correlationIdGenerator, int id, StatusCompra status)
        {

            _logger.LogWarning("Rodrigo Siqueira");
            

            var result = await _usuarioService.BuscarPorIdAsync(id);

            return result != null ? Ok(result) : NotFound();

            //var usuario = new Usuario(1, "Rodrigo Siqueira Silva", "rodrigosiqueirasilva@hormail.com", "123@teste", 0);
            //return Ok(usuario);
        }

        [HttpGet("BuscarTodosAsync")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> BuscarPorTodosAsync([FromServices] ICorrelationIdGenerator _correlationIdGenerator)
        {

            _logger.LogInformation("Rodrigo Siqueira");

            var result = await _usuarioService.BuscarTodosAsync();

            return result != null ? Ok(result) : NotFound();

            //var usuario = new Usuario(1, "Rodrigo Siqueira Silva", "rodrigosiqueirasilva@hormail.com", "123@teste", 0);
            //return Ok(usuario);
        }
    }
}
