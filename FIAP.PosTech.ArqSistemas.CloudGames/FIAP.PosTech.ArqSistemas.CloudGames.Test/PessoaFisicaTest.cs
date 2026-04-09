using Bogus;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace FIAP.PosTech.ArqSistemas.CloudGames.Test
{
    public class PessoaFisicaTest
    {
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(InfraTest.Url) };

        private PessoaFisica? GetPessoaFisicaFaker()
        {
            var _PessoaFisica = new Faker<PessoaFisica>("pt_BR").StrictMode(true)
                .RuleFor(j => j.Id, j => 0)
                .RuleFor(j => j.Nome, j => j.Name.FullName())
                .RuleFor(j => j.Email, (j, c) => j.Internet.Email().ToLower())
                .RuleFor(j => j.Senha, j => "!!13RoFiap")
                .RuleFor(j => j.Administrador, j => false)
                .Generate();
            return _PessoaFisica;
        }

        private async Task<PessoaFisica> BuscaPessoaFisicaIdAsync(int id, string tokenAdminValido)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);
            var response = await _client.GetAsync($"/PessoaFisica/BuscarPorId/{id}");
            var buscaPessoaFisica = await response.Content.ReadFromJsonAsync<PessoaFisica>();
            return buscaPessoaFisica;
        }

        [Fact]
        public async Task InclusaoPessoaFisicaAsync()
        {
            // Arrange
            var pessoaFisica = GetPessoaFisicaFaker();
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);

            // Act
            var response = await _client.PostAsJsonAsync("/PessoaFisica/Incluir", pessoaFisica);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoPessoaFisica = await response.Content.ReadFromJsonAsync<PessoaFisica>();
            Assert.NotNull(novoPessoaFisica);

            // Validação duplica verificando se existe na base
            var buscaPessoaFisica = await BuscaPessoaFisicaIdAsync(novoPessoaFisica.Id, tokenAdminValido);
            Assert.NotNull(buscaPessoaFisica);
            Assert.Equal(buscaPessoaFisica?.Id, novoPessoaFisica?.Id);
        }

        [Fact]
        public async Task AtualizacaoPessoaFisicaAsync()
        {
            // Arrange
            var pessoaFisica = GetPessoaFisicaFaker();
            var pessoaFisicaUpdate = GetPessoaFisicaFaker();
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);

            // Act
            var response = await _client.PostAsJsonAsync("/PessoaFisica/Incluir", pessoaFisica);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoPessoaFisica = await response.Content.ReadFromJsonAsync<PessoaFisica>();
            Assert.NotNull(novoPessoaFisica);

            // Validação duplica verificando se existe na base
            var buscaPessoaFisica = await BuscaPessoaFisicaIdAsync(novoPessoaFisica.Id, tokenAdminValido);
            Assert.NotNull(buscaPessoaFisica);
            Assert.Equal(buscaPessoaFisica?.Id, novoPessoaFisica?.Id);

            // Atualizando PessoaFisica
            buscaPessoaFisica.Nome = pessoaFisicaUpdate.Nome;
            await _client.PutAsJsonAsync("/PessoaFisica/Atualizar", buscaPessoaFisica);
            buscaPessoaFisica = await BuscaPessoaFisicaIdAsync(novoPessoaFisica.Id, tokenAdminValido);
            Assert.NotNull(buscaPessoaFisica);
            Assert.Equal(buscaPessoaFisica?.Nome, pessoaFisicaUpdate?.Nome);
        }

        [Fact]
        public async Task ExclusaoPessoaFisicaAsync()
        {
            // Arrange
            var pessoaFisica = GetPessoaFisicaFaker();
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);

            // Act
            var response = await _client.PostAsJsonAsync("/PessoaFisica/Incluir", pessoaFisica);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var novoPessoaFisica = await response.Content.ReadFromJsonAsync<PessoaFisica>();
            Assert.NotNull(novoPessoaFisica);

            // Validação duplica verificando se existe na base
            var buscaPessoaFisica = await BuscaPessoaFisicaIdAsync(novoPessoaFisica.Id, tokenAdminValido);
            Assert.NotNull(buscaPessoaFisica);
            Assert.Equal(buscaPessoaFisica?.Id, novoPessoaFisica?.Id);

            // Excluindo PessoaFisica
            response = await _client.DeleteAsync($"/PessoaFisica/Excluir/{novoPessoaFisica?.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync($"/PessoaFisica/BuscarPorId/{novoPessoaFisica?.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task BuscarTodosPessoaFisicaAsync()
        {
            // Arrange
            List<PessoaFisica> pessoaFisicas;
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenAdmin();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);

            // Act
            var response = await _client.GetAsync($"/PessoaFisica/BuscarTodos");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            pessoaFisicas = await response.Content.ReadFromJsonAsync<List<PessoaFisica>>();
            Assert.NotNull(pessoaFisicas);
            Assert.True(pessoaFisicas.Any());
        }
    }
}
