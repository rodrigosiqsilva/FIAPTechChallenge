using Bogus;
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

        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(InfraTest.Url) };

        private Jogo? GetJogoFaker()
        {
            var _jogo = new Faker<Jogo>("pt_BR").StrictMode(true)
                .RuleFor(j => j.Id, j => 0)
                .RuleFor(j => j.Nome, j => $"Jogo Teste {j.Random.Int(1, 1000000)}")
                .RuleFor(j => j.Ativo, j => true)
                .Generate();
            return _jogo;
        }

        private async Task<Jogo> BuscaJogoIdAsync(int id, string tokenAdminValido)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.GetAsync($"/Jogo/BuscarPorId/{id}");
            var buscaJogo = await response.Content.ReadFromJsonAsync<Jogo>();
            return buscaJogo;
        }

        [Fact]
        public async Task InclusaoJogoAsync()
        {
            // Arrange
            var jogo = GetJogoFaker();

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.PostAsJsonAsync("/Jogo/Incluir", jogo);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoJogo = await response.Content.ReadFromJsonAsync<Jogo>();
            Assert.NotNull(novoJogo);

            // Validação duplica verificando se existe na base
            var buscaJogo = await BuscaJogoIdAsync(novoJogo.Id, tokenAdminValido);
            Assert.NotNull(buscaJogo);
            Assert.Equal(buscaJogo?.Id, novoJogo?.Id);
        }

        [Fact]
        public async Task AtualizacaoJogoAsync()
        {
            // Arrange
            var jogo = GetJogoFaker();
            var jogoUpdate = GetJogoFaker();

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.PostAsJsonAsync("/Jogo/Incluir", jogo);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoJogo = await response.Content.ReadFromJsonAsync<Jogo>();
            Assert.NotNull(novoJogo);

            // Validação duplica verificando se existe na base
            var buscaJogo = await BuscaJogoIdAsync(novoJogo.Id, tokenAdminValido);
            Assert.NotNull(buscaJogo);
            Assert.Equal(buscaJogo?.Id, novoJogo?.Id);

            // Atualizando jogo
            buscaJogo.Nome = jogoUpdate.Nome;
            await _client.PutAsJsonAsync("/Jogo/Atualizar", buscaJogo);
            buscaJogo = await BuscaJogoIdAsync(novoJogo.Id, tokenAdminValido);
            Assert.NotNull(buscaJogo);
            Assert.Equal(buscaJogo?.Nome, jogoUpdate?.Nome);
        }

        [Fact]
        public async Task ExclusaoJogoAsync()
        {
            // Arrange
            var jogo = GetJogoFaker();

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.PostAsJsonAsync("/Jogo/Incluir", jogo);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoJogo = await response.Content.ReadFromJsonAsync<Jogo>();
            Assert.NotNull(novoJogo);

            // Validação duplica verificando se existe na base
            var buscaJogo = await BuscaJogoIdAsync(novoJogo.Id, tokenAdminValido);
            Assert.NotNull(buscaJogo);
            Assert.Equal(buscaJogo?.Id, novoJogo?.Id);

            // Excluindo jogo
            response = await _client.DeleteAsync($"/Jogo/Excluir/{novoJogo?.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync($"/Jogo/BuscarPorId/{novoJogo?.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task BuscarTodosJogoAsync()
        {
            // Arrange
            List<Jogo> jogos; 

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.GetAsync($"/Jogo/BuscarTodos");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jogos = await response.Content.ReadFromJsonAsync<List<Jogo>>();
            Assert.NotNull(jogos);
            Assert.True(jogos.Any());   
        }
    }
}
