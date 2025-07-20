using MangmentBS.Core.Entities;
using MangmentBS.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity,TKey> Repository<TEntity, TKey>() where TEntity :BaseEntity<TKey>;
    }
}
