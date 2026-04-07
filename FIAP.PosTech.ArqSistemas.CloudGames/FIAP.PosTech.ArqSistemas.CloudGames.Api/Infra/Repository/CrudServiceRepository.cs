using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository
{
    public class CrudServiceRepository<T> : ICrudRepository<T> where T : ClasseBase
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public CrudServiceRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Atualizar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public T BuscarPorId(int id)
        {
            return _dbSet.FirstOrDefault(entity => entity.Id == id);
        }

        public void Excluir(int id)
        {
            _dbSet.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public void Incluir(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        IList<T> ICrudRepository<T>.BuscarPorTodos()
        {
            return _dbSet.ToList();
        }
    }
}
