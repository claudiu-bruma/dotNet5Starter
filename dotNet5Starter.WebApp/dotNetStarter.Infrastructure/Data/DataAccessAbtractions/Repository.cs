using dotNet5Starter.Webapp.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotNet5Starter.Infrastructure.Data.DataAccessAbtractions
{
    public class Repository<TEntity> : IRepository<TEntity>
   where TEntity : class
    {
        private readonly Dotnet5StarterDbContext dataContext;

        public Repository(Dotnet5StarterDbContext context)
        {
            this.dataContext = context;
        }

        public async Task Add(TEntity entity)
        {
            await dataContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dataContext.Set<TEntity>().Update(entity);
        }

        public IQueryable<TEntity> Query()
        {
            return dataContext.Set<TEntity>();
        }

        public int Count()
        {
            return dataContext.Set<TEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return dataContext.Set<TEntity>().Count(where);
        }
    }
}
