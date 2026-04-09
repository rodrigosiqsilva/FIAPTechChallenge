using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Log;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    public class AutenticacaoController(IPessoaFisicaRepository pessoaFisicaRepository, IConfiguration configuration, 
        ICorrelationIdGenerator correlationIdGenerator, BaseLogger<PessoaFisicaController> logger) : Controller
    {

        private readonly IConfiguration _configuration = configuration;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository = pessoaFisicaRepository;
        private readonly ICorrelationIdGenerator _correlationIdGenerator = correlationIdGenerator;
        private readonly BaseLogger<PessoaFisicaController> _logger = logger;


        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            _logger.LogInformation($"Iniciando autenticação do usuário: {login.Email} senha: {login.Senha}");
            var result = _pessoaFisicaRepository.Autenticar(login.Email, login.Senha);

            if (result != null)
            {
                _logger.LogInformation($"Usuário autenticado com sucesso: {JsonSerializer.Serialize(result)}");
                    
                var token = GenerateToken(login.Email, result.Administrador ? "Admin" : "User");
                return Ok(new { token });
            }
            else
            {
                _logger.LogInformation($"Usuário não autenticado");
                return Unauthorized();
            }
        }

        private string GenerateToken(string email, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], 
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
