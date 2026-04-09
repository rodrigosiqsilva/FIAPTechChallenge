using Bogus;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
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
    public class PromocaoTest
    {
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(InfraTest.Url) };

        private Promocao? GetPromocaoFaker()
        {
            var _promocao = new Faker<Promocao>("pt_BR").StrictMode(true)
                .RuleFor(j => j.Id, j => 0)
                .RuleFor(j => j.Descricao, j => $"Promoção Teste {j.Random.Int(1, 1000000)}")
                .RuleFor(j => j.Ativa, j => true)
                .Generate();
            return _promocao;
        }

        private async Task<Promocao> BuscaPromocaoIdAsync(int id, string tokenAdminValido)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.GetAsync($"/Promocao/BuscarPorId/{id}");
            var buscaPromocao = await response.Content.ReadFromJsonAsync<Promocao>();
            return buscaPromocao;
        }

        [Fact]
        public async Task InclusaoPromocaoAsync()
        {
            // Arrange
            var promocao = GetPromocaoFaker();

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.PostAsJsonAsync("/Promocao/Incluir", promocao);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoPromocao = await response.Content.ReadFromJsonAsync<Promocao>();
            Assert.NotNull(novoPromocao);

            // Validação duplica verificando se existe na base
            var buscaPromocao = await BuscaPromocaoIdAsync(novoPromocao.Id, tokenAdminValido);
            Assert.NotNull(buscaPromocao);
            Assert.Equal(buscaPromocao?.Id, novoPromocao?.Id);
        }

        [Fact]
        public async Task AtualizacaoPromocaoAsync()
        {
            // Arrange
            var promocao = GetPromocaoFaker();
            var promocaoUpdate = GetPromocaoFaker();

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.PostAsJsonAsync("/Promocao/Incluir", promocao);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoPromocao = await response.Content.ReadFromJsonAsync<Promocao>();
            Assert.NotNull(novoPromocao);

            // Validação duplica verificando se existe na base
            var buscaPromocao = await BuscaPromocaoIdAsync(novoPromocao.Id, tokenAdminValido);
            Assert.NotNull(buscaPromocao);
            Assert.Equal(buscaPromocao?.Id, novoPromocao?.Id);

            // Atualizando Promocao
            buscaPromocao.Descricao = promocaoUpdate.Descricao;
            await _client.PutAsJsonAsync("/Promocao/Atualizar", buscaPromocao);
            buscaPromocao = await BuscaPromocaoIdAsync(novoPromocao.Id, tokenAdminValido);
            Assert.NotNull(buscaPromocao);
            Assert.Equal(buscaPromocao?.Descricao, promocaoUpdate?.Descricao);
        }

        [Fact]
        public async Task ExclusaoPromocaoAsync()
        {
            // Arrange
            var promocao = GetPromocaoFaker();

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.PostAsJsonAsync("/Promocao/Incluir", promocao);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoPromocao = await response.Content.ReadFromJsonAsync<Promocao>();
            Assert.NotNull(novoPromocao);

            // Validação duplica verificando se existe na base
            var buscaPromocao = await BuscaPromocaoIdAsync(novoPromocao.Id, tokenAdminValido);
            Assert.NotNull(buscaPromocao);
            Assert.Equal(buscaPromocao?.Id, novoPromocao?.Id);

            // Excluindo Promocao
            response = await _client.DeleteAsync($"/Promocao/Excluir/{novoPromocao?.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync($"/Promocao/BuscarPorId/{novoPromocao?.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task BuscarTodosPromocaoAsync()
        {
            // Arrange
            List<Promocao> promocoes;

            // Act
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.GetAsync($"/Promocao/BuscarTodos");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            promocoes = await response.Content.ReadFromJsonAsync<List<Promocao>>();
            Assert.NotNull(promocoes);
            Assert.True(promocoes.Any());
        }
    }
}
