using Microsoft.EntityFrameworkCore;
using Route.Talaat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
    internal static class SpecificationEvaluator<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecifications<TEntity,TKey> spec)
        {
            var Query = inputQuery;
            
            if(spec.Criteria is not null)
            {
                Query = Query.Where(spec.Criteria);
            }
            if(spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            } else if (spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }

            if (spec.IsPaginationEnabled)
            {
                Query = Query.Skip(spec.Skip).Take(spec.Take);
            }

            Query = spec.Includes.Aggregate(Query,(CurrentQuery,IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return Query;
        }

    }
}
