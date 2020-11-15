using System.Threading.Tasks;

namespace dotNet5Starter.Infrastructure.Data.DataAccessAbtractions
{
    public interface IUnitOfWork
    {
        Task Commit();
    }    
}
