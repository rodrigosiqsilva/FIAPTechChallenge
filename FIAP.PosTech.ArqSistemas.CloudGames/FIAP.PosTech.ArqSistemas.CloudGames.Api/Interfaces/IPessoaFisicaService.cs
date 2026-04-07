using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IPessoaFisicaService
    {
        Task IncluirAsync(PessoaFisica usuario);

        Task AtualizarAsync(PessoaFisica usuario);

        Task ExcluirAsync(int id);

        Task<PessoaFisica> BuscarPorIdAsync(int id);

        Task<List<PessoaFisica>> BuscarTodosAsync();
    }
}
