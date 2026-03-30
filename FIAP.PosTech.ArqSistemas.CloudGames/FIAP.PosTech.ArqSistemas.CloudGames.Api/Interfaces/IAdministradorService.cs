using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IAdministradorService
    {
        Task IncluirAsync(Administrador administrador);

        Task AtualizarAsync(Administrador administrador);

        Task ExcluirAsync(int id);

        Task<Administrador> BuscarPorIdAsync(int id);

        Task<List<Administrador>> BuscarPorTodosAsync();
    }
}
