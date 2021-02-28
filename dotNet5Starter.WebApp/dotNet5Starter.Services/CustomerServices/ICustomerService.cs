using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNet5Starter.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<int> Add(CustomerDto newCompany);
        IEnumerable<CustomerDto> GetCustomers();
        CustomerDto GettCustomerById(int id);
        Task Update(CustomerDto customerDto);
    }
}
