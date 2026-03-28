
using FIAP.PosTech.ArqSistemas.CloudGames.Domain;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IAdministrador
    {
        Task IncluirAsync(Administrador administrador);

        Task AtualizarAsync(Administrador administrador);

        Task ExcluirAsync(int id);

        Task<Administrador> BuscarPorIdAsync(int id);

        Task<List<Administrador>> BuscarPorTodosAsync();
    }
}
