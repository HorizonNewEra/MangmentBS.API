﻿using MangmentBS.Core.Entities;
using MangmentBS.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Repository
{
    public class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications) 
        {
            var query = inputQuery;
            if (specifications.Criteria != null)
            {
                query = query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy != null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDescending != null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }
            if (specifications.IsPagingEnabled)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            foreach (var include in specifications.Includes)
            {
                query = query.Include(include);
            }
            return query;
        }
        public static async Task<int> GetQueryCount(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications)
        {
            var query = inputQuery;
            if (specifications.Criteria != null)
            {
                query = query.Where(specifications.Criteria);
            }
            return await query.CountAsync();
        }
    }
}
