using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers
{
    public class AutenticacaoController(IConfiguration configuration) : Controller
    {

        private readonly IConfiguration _configuration = configuration;


        [HttpPost("login")]
        public IActionResult Login(string email, string senha)
        {
            if ((email == "rodrigosiqueirasilva@hotmail.com") && (senha == "123@abC!!"))
            {
                var token = GenerateToken(email, "Admin");
                return Ok(new { token });
            }
            else if ((email == "fisioterapeuta@hotmail.com") && (senha == "123@abC!!"))
            {
                var token = GenerateToken(email, "User");
                return Ok(new { token });
            }
            else
            {
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
