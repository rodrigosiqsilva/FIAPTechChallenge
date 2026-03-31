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

        async Task<Usuario> IUsuarioService.BuscarPorIdAsync(int id)
        {
            var usuario = new Usuario { Nome = "Rodrigo Siqueira", Email = "rodrigosiqueirasilva@hotmail.com", Senha = "xxxx", Id = 1, Administrador = true };
            return usuario;
        }

        async Task<List<Usuario>> IUsuarioService.BuscarTodosAsync()
        {
            var usuarios = new List<Usuario>();

            usuarios.Add(new Usuario { Nome = "Rodrigo Siqueira", Email = "rodrigosiqueirasilva@hotmail.com", Senha = "xxxx", Id = 1, Administrador = true});
            usuarios.Add(new Usuario { Nome = "Renata Punzi Monteiro", Email = "fisioterapeuta@hotmail.com", Senha = "yyy", Id = 2, Administrador = false });

            return usuarios;
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
