using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaFisicaController(IPessoaFisicaService usuarioService, ICorrelationIdGenerator correlationIdGenerator,
        BaseLogger<PessoaFisicaController> logger) :
        ControllerBase
    {

        private readonly IPessoaFisicaService _usuarioService = usuarioService;
        private readonly ICorrelationIdGenerator _correlationIdGenerator = correlationIdGenerator;
        private readonly BaseLogger<PessoaFisicaController> _logger = logger;

        [HttpPost("IncluirAsync")]
        [Authorize(Policy = "Admin")]
        public IActionResult Incluir([FromServices] ICorrelationIdGenerator correlationIdGenerator, [FromBody] PessoaFisica pessoaFisica,
            [FromServices] IValidator<PessoaFisica> validator)
        {
            var validationResult = validator.Validate(pessoaFisica);

            if (!validationResult.IsValid)
            {
                // Retorna os erros formatados
                return BadRequest(validationResult.Errors);
            }

            return Accepted(pessoaFisica);
        }

        [HttpPut("AtualizarAsync")]
        public IActionResult Atualizar([FromServices] ICorrelationIdGenerator correlationIdGenerator, [FromBody] PessoaFisica pessoaFisica,
            [FromServices] IValidator<PessoaFisica> validator)
        {
            var validationResult = validator.Validate(pessoaFisica);

            if (!validationResult.IsValid)
            {
                // Retorna os erros formatados
                return BadRequest(validationResult.Errors);
            }

            return NoContent();
        }

        [HttpDelete("ExcluirAsync")]
        public IActionResult Excluir([FromServices] ICorrelationIdGenerator correlationIdGenerator, int id)
        {
            return NoContent(); // Retorna 204 (sucesso sem conteúdo)
        }

        [HttpGet("BuscarPorIdAsync/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> BuscarPorIdAsync([FromServices] ICorrelationIdGenerator correlationIdGenerator, int id)
        {

            _logger.LogWarning("Rodrigo Siqueira");
            

            var result = await _usuarioService.BuscarPorIdAsync(id);

            return result != null ? Ok(result) : NotFound();

            //var usuario = new Usuario(1, "Rodrigo Siqueira Silva", "rodrigosiqueirasilva@hormail.com", "123@teste", 0);
            //return Ok(usuario);
        }

        [HttpGet("BuscarTodosAsync")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> BuscarPorTodosAsync([FromServices] ICorrelationIdGenerator correlationIdGenerator)
        {

            _logger.LogInformation("Rodrigo Siqueira");

            var result = await _usuarioService.BuscarTodosAsync();

            return result != null ? Ok(result) : NotFound();

            //var usuario = new Usuario(1, "Rodrigo Siqueira Silva", "rodrigosiqueirasilva@hormail.com", "123@teste", 0);
            //return Ok(usuario);
        }
    }
}
