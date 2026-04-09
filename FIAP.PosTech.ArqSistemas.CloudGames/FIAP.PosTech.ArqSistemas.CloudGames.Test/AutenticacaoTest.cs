using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using System.Net;
using System.Net.Http.Json;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Test
{
    public class AutenticacaoTest
    {

        private readonly HttpClient _client;

        public AutenticacaoTest()
        {
            _client = new() { BaseAddress = new Uri(InfraTest.Url) };
        }

        [Fact]
        public async Task LoginValidoAsync()
        {
            // Arrange
            var login = InfraTest.GetLoginValido;

            // Act
            var response = await _client.PostAsJsonAsync("/Login", login);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var credencial = await response.Content.ReadFromJsonAsync<RetornoLogin>();
            Assert.NotNull(credencial);
            Assert.NotNull(credencial.Token);
        }

        [Fact]
        public async Task LoginInvalidoAsync()
        {
            // Arrange
            var login = InfraTest.GetLoginInvalido;

            // Act
            var response = await _client.PostAsJsonAsync("/Login", login);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}