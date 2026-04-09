using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using System.Runtime.CompilerServices;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface IPessoaFisicaRepository : ICrudRepository<PessoaFisica>
    {

        PessoaFisica Autenticar(string email, string senha);

    }
}
