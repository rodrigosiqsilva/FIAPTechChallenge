using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Log;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogoController(IJogoRepository jogoRepository, BaseLogger<PessoaFisicaController> logger) :
        ControllerBase
    {
        private readonly IJogoRepository _jogoRepository = jogoRepository;
        private readonly BaseLogger<PessoaFisicaController> _logger = logger;

        [HttpGet("BuscarTodos")]
        [Authorize(Roles = "User,Admin")]
        public Task<IActionResult> BuscarTodos()
        {
            _logger.LogInformation("Iniciando busca por todos os jogos");
            var result = _jogoRepository.BuscarTodos();

            if ((result != null) && (result.Count > 0))
            {
                _logger.LogInformation($"Busca realizada com sucesso: {JsonSerializer.Serialize(result)}");
                return Task.FromResult<IActionResult>(Ok(result));
            }
            else
            {
                _logger.LogInformation($"Busca realizada com sucesso: Não há jogos cadastrados");
                return Task.FromResult<IActionResult>(NotFound());
            }
        }

        [HttpGet("BuscarPorId/{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public Task<IActionResult> BuscarPorId([FromRoute] int id)
        {
            _logger.LogInformation($"Iniciando busca por jogo id {id}");
            var result = _jogoRepository.BuscarPorId(id);

            if (result != null)
            {
                _logger.LogInformation($"Busca realizada com sucesso: {JsonSerializer.Serialize(result)}");
                return Task.FromResult<IActionResult>(Ok(result));
            }
            else
            {
                _logger.LogInformation($"Busca realizada com sucesso: Jogo não cadastrado");
                return Task.FromResult<IActionResult>(NotFound());
            }
        }

        [HttpPost("Incluir")]
        [Authorize(Policy = "Admin")]
        public IActionResult Incluir([FromBody] Jogo jogo)
        {
            try
            {
                _logger.LogInformation($"Iniciando inclusão de jogo: {JsonSerializer.Serialize(jogo)}");
                _jogoRepository.Incluir(jogo);
                _logger.LogInformation($"Dados do jogo adicionados com sucesso");

                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("Atualizar")]
        [Authorize(Policy = "Admin")]
        public IActionResult Atualizar([FromBody] Jogo jogo)
        {
            try
            {
                _logger.LogInformation($"Iniciando atualização do jogo: {JsonSerializer.Serialize(jogo)}");
   
                var _jogo = _jogoRepository.BuscarPorId(jogo.Id);

                if (_jogo == null)
                {
                    _logger.LogInformation($"Jogo não cadastrado para realizar alteração");
                    return NotFound();
                }

                _jogo.Ativo = jogo.Ativo;
                _jogo.Nome = jogo.Nome;

                _jogoRepository.Atualizar(_jogo);
                _logger.LogInformation($"Dados do jogo atualizados com sucesso");

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
                _logger.LogInformation($"Iniciando exclusão do jogo id : {id}");

                var jogo = _jogoRepository.BuscarPorId(id);

                if (jogo == null)
                {
                    _logger.LogInformation($"Jogo não cadastrado para realizar exclusão");
                    return NotFound();
                }

                _jogoRepository.Excluir(id);
                _logger.LogInformation($"Dados do jogo excluídos com sucesso:  {JsonSerializer.Serialize(jogo)}");

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
