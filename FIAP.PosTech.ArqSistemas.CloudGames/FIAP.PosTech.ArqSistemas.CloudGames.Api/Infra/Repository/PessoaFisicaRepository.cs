using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository
{
    public class PessoaFisicaRepository : CrudRepository<PessoaFisica>, IPessoaFisicaRepository
    {
        public PessoaFisicaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public PessoaFisica Autenticar(string email, string senha)
        {
            return _dbSet.FirstOrDefault(entity => entity.Email == email && entity.Senha == senha);
        }
    }
}
