using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Log;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromocaoController(IPromocaoRepository promocaoRepository, BaseLogger<PessoaFisicaController> logger) :
        ControllerBase
    {
        private readonly IPromocaoRepository _promocaoRepository = promocaoRepository;
        private readonly BaseLogger<PessoaFisicaController> _logger = logger;

        [HttpGet("BuscarTodos")]
        [Authorize(Policy = "Admin")]
        public Task<IActionResult> BuscarTodos()
        {
            _logger.LogInformation("Iniciando busca por todas as promoções");
            var result = _promocaoRepository.BuscarTodos();

            if ((result != null) && (result.Count > 0))
            {
                _logger.LogInformation($"Busca realizada com sucesso: {JsonSerializer.Serialize(result)}");
                return Task.FromResult<IActionResult>(Ok(result));
            }
            else
            {
                _logger.LogInformation($"Busca realizada com sucesso: Não há promoções cadastradas");
                return Task.FromResult<IActionResult>(NotFound());
            }
        }

        [HttpGet("BuscarPorId/{id:int}")]
        [Authorize(Policy = "Admin")]
        public Task<IActionResult> BuscarPorId([FromRoute] int id)
        {
            _logger.LogInformation($"Iniciando busca por promoção id {id}");
            var result = _promocaoRepository.BuscarPorId(id);

            if (result != null)
            {
                _logger.LogInformation($"Busca realizada com sucesso: {JsonSerializer.Serialize(result)}");
                return Task.FromResult<IActionResult>(Ok(result));
            }
            else
            {
                _logger.LogInformation($"Busca realizada com sucesso: Promoção não cadastrada");
                return Task.FromResult<IActionResult>(NotFound());
            }
        }

        [HttpPost("Incluir")]
        [Authorize(Policy = "Admin")]
        public IActionResult Incluir([FromBody] Promocao promocao)
        {
            try
            {
                _logger.LogInformation($"Iniciando inclusão de promoção: {JsonSerializer.Serialize(promocao)}");
                _promocaoRepository.Incluir(promocao);
                _logger.LogInformation($"Dados da promoção adicionados com sucesso");

                return Ok(promocao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("Atualizar")]
        [Authorize(Policy = "Admin")]
        public IActionResult Atualizar([FromBody] Promocao promocao)
        {
            try
            {
                _logger.LogInformation($"Iniciando atualização da promoção: {JsonSerializer.Serialize(promocao)}");

                var _promocao = _promocaoRepository.BuscarPorId(promocao.Id);

                if (_promocao == null)
                {
                    _logger.LogInformation($"Promoção não cadastrada para realizar alteração");
                    return NotFound();
                }

                _promocao.Ativa = promocao.Ativa;
                _promocao.Descricao = promocao.Descricao;

                _promocaoRepository.Atualizar(_promocao);
                _logger.LogInformation($"Dados da promoção atualizados com sucesso");

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("Excluir/{id:int}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Excluir([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando exclusão da promoção id : {id}");

                var jogo = _promocaoRepository.BuscarPorId(id);

                if (jogo == null)
                {
                    _logger.LogInformation($"Promoção não cadastrado para realizar exclusão");
                    return NotFound();
                }

                _promocaoRepository.Excluir(id);
                _logger.LogInformation($"Dados da promoção excluídos com sucesso: {JsonSerializer.Serialize(jogo)}");

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
