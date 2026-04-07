using FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Microsoft.Extensions.Logging;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Services
{
    public class PessoaFisicaService(BaseLogger<PessoaFisicaController> logger) : IPessoaFisicaService
    {
        private readonly BaseLogger<PessoaFisicaController> _logger = logger;

        Task IPessoaFisicaService.AtualizarAsync(PessoaFisica pessoaFisica)
        {
            _logger.LogError("Erro na execução"); 
            return Task.CompletedTask;

            throw new NotImplementedException();
        }

        async Task<PessoaFisica> IPessoaFisicaService.BuscarPorIdAsync(int id)
        {
            var pessoaFisica = new PessoaFisica { Nome = "Rodrigo Siqueira", Email = "rodrigosiqueirasilva@hotmail.com", Senha = "xxxx", Id = 1};
            return pessoaFisica;
        }

        async Task<List<PessoaFisica>> IPessoaFisicaService.BuscarTodosAsync()
        {
            var pessoasFisica = new List<PessoaFisica>();

            pessoasFisica.Add(new PessoaFisica { Nome = "Rodrigo Siqueira", Email = "rodrigosiqueirasilva@hotmail.com", Senha = "xxxx", Id = 1});
            pessoasFisica.Add(new PessoaFisica { Nome = "Renata Punzi Monteiro", Email = "fisioterapeuta@hotmail.com", Senha = "yyy", Id = 2});

            return pessoasFisica;
        }

        Task IPessoaFisicaService.ExcluirAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IPessoaFisicaService.IncluirAsync(PessoaFisica pessoaFisica)
        {
            throw new NotImplementedException();
        }
    }
}
