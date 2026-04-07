using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;


namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository
{
    public class JogoRepository : CrudRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
