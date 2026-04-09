using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Test
{
    public class InfraTest 
    {
        public const string Url = "https://localhost:7140";

        public static Login GetLoginValido => 
            new Login { Email = "rodrigosiqueirasilva@hotmail.com", Senha = "!!13RoFiap" };

        public static Login GetLoginInvalido =>
            new Login { Email = "rodrigosiqueirasilva@hotmail.com", Senha = "13RoFiap" };

        public static Login GetLoginAdmin =>
            new Login { Email = "rodrigosiqueirasilva@hotmail.com", Senha = "!!13RoFiap" };

        public static Login GetLoginUser =>
            new Login { Email = "renatapunzi@hotmail.com", Senha = "!!13ReFiap" };

        public readonly HttpClient _client;

        public InfraTest()
        {
            _client = new() { BaseAddress = new Uri(InfraTest.Url) };
        }

        public async Task<string?> GetTokenAdmin()
        {
            var login = InfraTest.GetLoginAdmin;
            var response = await _client.PostAsJsonAsync("/login", login);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var credencial = await response.Content.ReadFromJsonAsync<RetornoLogin>();
                return credencial.Token;
            }
            return null;
        }

        public async Task<string?> GetTokenUser()
        {
            var login = InfraTest.GetLoginUser;
            var response = await _client.PostAsJsonAsync("/login", login);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var credencial = await response.Content.ReadFromJsonAsync<RetornoLogin>();
                return credencial.Token;
            }
            return null;
        }
    }
}
