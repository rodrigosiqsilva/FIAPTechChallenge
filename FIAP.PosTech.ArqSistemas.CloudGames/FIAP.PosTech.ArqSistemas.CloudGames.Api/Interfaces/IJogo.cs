using FIAP.PosTech.ArqSistemas.CloudGames.Domain;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IJogo
    {
        Task IncluirAsync(Jogo jogo);

        Task AtualizarAsync(Jogo jogo);

        Task ExcluirAsync(int id);

        Task<Jogo> BuscarPorIdAsync(int id);

        Task<List<Jogo>> BuscarPorTodosAsync();
    }
}
