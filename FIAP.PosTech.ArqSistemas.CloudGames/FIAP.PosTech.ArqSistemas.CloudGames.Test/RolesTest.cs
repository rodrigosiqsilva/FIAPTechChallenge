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
    public class RolesTest
    {

        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(InfraTest.Url) };

        [Fact]
        public async Task AutorizadoAsync()
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

        [Fact]
        public async Task NaoAutorizadoAsync()
        {
            // Arrange
            var infraTest = new InfraTest();
            var tokenAdminValido = await infraTest.GetTokenUser();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdminValido);

            // Act
            var response = await _client.GetAsync($"/PessoaFisica/BuscarTodos");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
