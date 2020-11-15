using dotNet5Starter.Webapp.Infrastructure;
using System.Threading.Tasks;

namespace dotNet5Starter.Infrastructure.Data.DataAccessAbtractions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dotnet5StarterDbContext context;

        public UnitOfWork(Dotnet5StarterDbContext context)
        {
            this.context = context;
        }


        public async Task Commit()
        {
            try
            {
               await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
