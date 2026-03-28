using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        Task IUsuarioService.AtualizarAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        Task<Usuario> IUsuarioService.BuscarPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Usuario>> IUsuarioService.BuscarPorTodosAsync()
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
