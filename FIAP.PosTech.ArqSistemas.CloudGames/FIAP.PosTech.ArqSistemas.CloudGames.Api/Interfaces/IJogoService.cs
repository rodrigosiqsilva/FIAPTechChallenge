using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IJogoService
    {
        Task IncluirAsync(Jogo jogo);

        Task AtualizarAsync(Jogo jogo);

        Task ExcluirAsync(int id);

        Task<Jogo> BuscarPorIdAsync(int id);

        Task<List<Jogo>> BuscarPorTodosAsync();
    }
}
