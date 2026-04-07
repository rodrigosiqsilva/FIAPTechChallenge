using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository
{
    public class PessoaFisicaRepository : CrudServiceRepository<PessoaFisica>, IPessoaFisicaRepository
    {
        public PessoaFisicaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
