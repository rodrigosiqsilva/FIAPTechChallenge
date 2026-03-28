using FIAP.PosTech.ArqSistemas.CloudGames.Domain;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IUsuario
    {
        Task IncluirAsync(Usuario usuario);

        Task AtualizarAsync(Usuario usuario);

        Task ExcluirAsync(int id);

        Task<Usuario> BuscarPorIdAsync(int id);

        Task<List<Usuario>> BuscarPorTodosAsync();
    }
}
