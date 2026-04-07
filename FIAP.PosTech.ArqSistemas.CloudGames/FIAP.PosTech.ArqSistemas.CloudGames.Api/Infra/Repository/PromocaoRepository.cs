using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository
{
    public class PromocaoRepository : CrudRepository<Promocao>, IPromocaoRepository
    {
        public PromocaoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
