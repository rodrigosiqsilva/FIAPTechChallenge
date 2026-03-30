using FIAP.PosTech.ArqSistemas.CloudGames.Api.Controllers;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Microsoft.Extensions.Logging;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Services
{
    public class UsuarioService(BaseLogger<UsuarioController> logger) : IUsuarioService
    {
        private readonly BaseLogger<UsuarioController> _logger = logger;

        Task IUsuarioService.AtualizarAsync(Usuario usuario)
        {
            _logger.LogError("Erro na execução"); 
            return Task.CompletedTask;

            throw new NotImplementedException();
        }

        Task<Usuario> IUsuarioService.BuscarPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Usuario>> IUsuarioService.BuscarTodosAsync()
        {
            throw new NotImplementedException();
        }

        Task IUsuarioService.ExcluirAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IUsuarioService.IncluirAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
