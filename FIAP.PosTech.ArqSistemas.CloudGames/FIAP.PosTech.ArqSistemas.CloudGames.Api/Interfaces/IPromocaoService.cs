using FIAP.PosTech.ArqSistemas.CloudGames.Domain;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IPromocaoService
    {
        Task IncluirAsync(Promocao promocao);

        Task AtualizarAsync(Promocao promocao);

        Task ExcluirAsync(int id);

        Task<Promocao> BuscarPorIdAsync(int id);

        Task<List<Promocao>> BuscarPorTodosAsync();
    }
}
