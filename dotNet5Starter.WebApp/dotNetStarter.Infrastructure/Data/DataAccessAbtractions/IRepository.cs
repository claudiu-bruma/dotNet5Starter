using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5Starter.Infrastructure.Data.DataAccessAbtractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> Query();
        int Count();
        int Count(Expression<Func<TEntity, bool>> where);

    }
}
