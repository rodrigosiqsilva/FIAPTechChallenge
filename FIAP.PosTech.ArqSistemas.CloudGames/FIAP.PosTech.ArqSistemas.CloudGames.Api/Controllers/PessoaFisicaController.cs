using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Log;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaFisicaController(IPessoaFisicaRepository pessoaFisicaRepository, BaseLogger<PessoaFisicaController> logger) :
        ControllerBase
    {
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository = pessoaFisicaRepository;
        private readonly BaseLogger<PessoaFisicaController> _logger = logger;


        [HttpGet("BuscarTodos")]
        [Authorize(Policy = "Admin")]
        public Task<IActionResult> BuscarTodos()
        {
            _logger.LogInformation("Iniciando busca por todos as pessoas físicas");
            var result = _pessoaFisicaRepository.BuscarTodos();

            if ((result != null) && (result.Count > 0))
            {
                _logger.LogInformation($"Busca realizada com sucesso: {JsonSerializer.Serialize(result)}");
                return Task.FromResult<IActionResult>(Ok(result)); 
            }
            else
            {
                _logger.LogInformation($"Busca realizada com sucesso: Não há pessoas físicas cadastradas");
                return Task.FromResult<IActionResult>(NotFound());
            }    
        }

        [HttpGet("BuscarPorId/{id:int}")]
        [Authorize(Policy = "Admin")]
        public Task<IActionResult> BuscarPorId([FromRoute] int id)
        {
            _logger.LogInformation($"Iniciando busca por pessoa física id {id}");
            var result = _pessoaFisicaRepository.BuscarPorId(id);

            if (result != null) 
            {
                _logger.LogInformation($"Busca realizada com sucesso: {JsonSerializer.Serialize(result)}");
                return Task.FromResult<IActionResult>(Ok(result));
            }
            else
            {
                _logger.LogInformation($"Busca realizada com sucesso: Pessoa física não cadastrada");
                return Task.FromResult<IActionResult>(NotFound());
            }
        }

        [HttpPost("Incluir")]
        [Authorize(Policy = "Admin")]
        public IActionResult Incluir([FromBody] PessoaFisica pessoaFisica, [FromServices] IValidator<PessoaFisica> validator)
        {
            try
            {
                _logger.LogInformation($"Iniciando inclusão de pessoa física validando os dados: {JsonSerializer.Serialize(pessoaFisica)}");
                var validationResult = validator.Validate(pessoaFisica);

                if (!validationResult.IsValid)
                {
                    _logger.LogInformation($"Dados da pessoa física incorretos: {JsonSerializer.Serialize(validationResult.Errors)}");
                    return BadRequest(validationResult.Errors);
                }

                _pessoaFisicaRepository.Incluir(pessoaFisica);
                _logger.LogInformation($"Dados da pessoa física adicionados com sucesso");

                return Ok(pessoaFisica);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("Atualizar")]
        [Authorize(Policy = "Admin")]
        public IActionResult Atualizar([FromBody] PessoaFisica pessoaFisica, [FromServices] IValidator<PessoaFisica> validator)
        {
            try
            {
                _logger.LogInformation($"Iniciando atualização de pessoa física: {JsonSerializer.Serialize(pessoaFisica)}");
                var validationResult = validator.Validate(pessoaFisica);

                if (!validationResult.IsValid)
                {
                    _logger.LogInformation($"Dados da pessoa física incorretos dados não podem ser atualizados: {JsonSerializer.Serialize(validationResult.Errors)}");
                    return BadRequest(validationResult.Errors);
                }

                var resultPf = _pessoaFisicaRepository.BuscarPorId(pessoaFisica.Id);

                if (resultPf == null)
                {
                    _logger.LogInformation($"Pessoa física não cadastrada para realizar alteração");
                    return NotFound();
                }

                resultPf.Senha = pessoaFisica.Senha;
                resultPf.Nome = pessoaFisica.Nome;
                resultPf.Administrador = pessoaFisica.Administrador;
                resultPf.Email = pessoaFisica.Email;    

                _pessoaFisicaRepository.Atualizar(resultPf);
                _logger.LogInformation($"Dados da pessoa física atualizados com sucesso");

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
                _logger.LogInformation($"Iniciando exclusão de pessoa física id : {id}");
               
                var resultPf = _pessoaFisicaRepository.BuscarPorId(id);

                if (resultPf == null)
                {
                    _logger.LogInformation($"Pessoa física não cadastrada para realizar exclusão");
                    return NotFound();
                }

                _pessoaFisicaRepository.Excluir(id);
                _logger.LogInformation($"Dados da pessoa física excluídos com sucesso:  {JsonSerializer.Serialize(resultPf)}");

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
