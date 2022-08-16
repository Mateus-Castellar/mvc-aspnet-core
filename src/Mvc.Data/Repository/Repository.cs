using Microsoft.EntityFrameworkCore;
using Mvc.Business.Interfaces;
using Mvc.Business.Models;
using Mvc.Data.Context;
using System.Linq.Expressions;

namespace Mvc.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly AppMvcContext _context;
        protected readonly DbSet<T> _dbSet;//atalho

        public Repository(AppMvcContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task Adicionar(T entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(T entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<T> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            //_dbSet.Remove(await _dbSet.FindAsync(id)); => mais processamento

            var entity = new T { Id = id };
            _dbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}