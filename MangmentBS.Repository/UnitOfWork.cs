using MangmentBS.Core;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Repositories.Interfaces;
using MangmentBS.Repository.Data.Contexts;
using MangmentBS.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private Hashtable _repository;
        public UnitOfWork(AppDbContext _context)
        {
            context = _context;
            _repository = new Hashtable();
        }
        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (!_repository.ContainsKey(typeof(TEntity).Name))
            {
                var repository = new GenericRepository<TEntity, TKey>(context);
                _repository.Add(typeof(TEntity).Name, repository);
            }
            return _repository[typeof(TEntity).Name] as IGenericRepository<TEntity, TKey>;
        }

        
    }
}
