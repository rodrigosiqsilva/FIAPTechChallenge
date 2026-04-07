using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces
{
    public interface ICrudRepository<T> where T : ClasseBase
    {
        void Incluir(T entidade);

        void Atualizar(T entidade);

        void Excluir(int id);

        T BuscarPorId(int id);
        IList<T> BuscarPorTodos();
    }
}
