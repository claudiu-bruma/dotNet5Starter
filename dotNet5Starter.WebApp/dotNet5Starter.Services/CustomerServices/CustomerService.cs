using AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataAccessAbtractions;
using dotNet5Starter.Webapp.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet5Starter.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> _customerRepo { get; init; }
        private IUnitOfWork _unitOfWork { get; init; }
        public IMapper _mapper { get; init; }

        public CustomerService(IRepository<Customer> customerRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Add(CustomerDto newCompany)
        {
            var customerEntity = _mapper.Map<Customer>(newCompany);
            await _customerRepo.Add(customerEntity);
            await _unitOfWork.Commit();
            return customerEntity.Id;
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
           return _customerRepo.Query().Select(x => _mapper.Map<CustomerDto>(x));            
        }

        public CustomerDto GettCustomerById(int id)
        {
            if (!_customerRepo.Query().Any(x => x.Id == id))
            {
                throw new ArgumentException("Company Id does not exist");
            }
            return _customerRepo.Query()
                .Where(x => x.Id == id)
                .Select(x =>
                   _mapper.Map<CustomerDto>(x)
                ).FirstOrDefault();
        }

        public async Task Update(CustomerDto customerDto)
        {
            if (!_customerRepo.Query().Any(x => x.Id == customerDto.Id))
            {
                throw new ArgumentException("Company Id does not exist");
            }

            var initialRecord = _customerRepo.Query().Where(x => x.Id == customerDto.Id).FirstOrDefault();
            initialRecord = _mapper.Map<Customer>(customerDto);
            _customerRepo.Update(initialRecord);
            await _unitOfWork.Commit();
        }

    }
}
