using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Test
{
    public class JogoTest
    {

        public readonly HttpClient _client;

        public JogoTest()
        {
            _client = new() { BaseAddress = new Uri(InfraTest.Url) };
        }

        [Fact]
        public async Task InclusaoJogoAsync()
        {
            // Arrange
            var infraTest = new InfraTest();
            var tokenAdminValido = infraTest.GetTokenAdmin();
            var jogo = new Jogo { Nome = "Teste", Ativo = true };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenAdminValido);
            var response = await _client.PostAsJsonAsync("/Jogo/Incluir", jogo);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoJogo = await response.Content.ReadFromJsonAsync<Jogo>();
            Assert.NotNull(novoJogo);

            // Validação duplica verificando se existe na base
            response = await _client.GetAsync($"/Jogo/BuscarPorId/{novoJogo?.Id}");
            var buscaJogo = await response.Content.ReadFromJsonAsync<Jogo>();
            Assert.NotNull(buscaJogo);
            Assert.Equal(buscaJogo?.Id, novoJogo?.Id);
        }
    }
}
