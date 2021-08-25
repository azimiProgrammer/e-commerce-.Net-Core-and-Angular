using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Data
{
    public class SpesificationEvaluator<TEntinty> where TEntinty : BaseEntity
    {
        public static IQueryable<TEntinty> GetQuery(IQueryable<TEntinty> inputQuery, ISpecification<TEntinty> spec)
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (!string.IsNullOrEmpty(spec.DynamicOrderBy))
            {
                query = query.OrderBy(spec.DynamicOrderBy);
            }

            if(spec.IsPagingEnabled)
            {
                query = query
                    .Skip(spec.Skip)
                    .Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
