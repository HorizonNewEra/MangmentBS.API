using MangmentBS.Core.Entities;
using MangmentBS.Core.Repositories.Interfaces;
using MangmentBS.Core.Specifications;
using MangmentBS.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task AddAsync(TEntity entity)
        {
            await context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            context.Remove(entity);
        }
        private IQueryable<TEntity> ApplySpecifications (ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(context.Set<TEntity>(), specifications);
        }
        public Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQueryCount(context.Set<TEntity>(), specifications); 
        }
    }
}
