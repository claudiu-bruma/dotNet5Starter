using AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataAccessAbtractions;
using dotNet5Starter.Infrastructure.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet5Starter.Services.CompanyServices
{
    public class CompanyService : ICompanyService
    {
        private IRepository<Company> _companyRepo { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }
        public CompanyService(IRepository<Company> companyRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _companyRepo = companyRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Add(CompanyDto newCompany)
        {
            if (_companyRepo.Query().Any(x => x.ISIN == newCompany.Isin))
            {
                throw new ArgumentException("Company Isin already exist");
            }
            var companyEntity = _mapper.Map<Company>(newCompany);
            await _companyRepo.Add(companyEntity);
            await _unitOfWork.Commit();
            return companyEntity.Id;
        }

        public IEnumerable<CompanyDto> GetCompanies()
        {
            return _companyRepo.Query().Select(x => new CompanyDto()
            {
                Exchange = x.Exchange,
                Id = x.Id,
                Isin = x.ISIN,
                Name = x.Name,
                StockTicker = x.StockTicker,
                Website = x.Website
            }
            );
        }

        public CompanyDto GetCompanyById(int id)
        {
            if (!_companyRepo.Query().Any(x => x.Id == id))
            {
                throw new ArgumentException("Company Id does not exist");
            }
            return _companyRepo.Query()
                .Where(x => x.Id == id)
                .Select(x =>
                    _mapper.Map<CompanyDto>(x)
                ).FirstOrDefault();
        }

        public CompanyDto GetCompanyByIsin(string isin)
        {
            if (!_companyRepo.Query().Any(x => x.ISIN == isin))
            {
                throw new ArgumentException("Company Isin does not exist");
            }
            return _companyRepo.Query()
                .Where(x => x.ISIN == isin)
                .Select(x =>
                    _mapper.Map<CompanyDto>(x)
                ).FirstOrDefault();
        }

        public async Task Update(CompanyDto companyDto)
        {
            if (!_companyRepo.Query().Any(x => x.Id == companyDto.Id))
            {
                throw new ArgumentException("Company Id does not exist");
            }
            if (_companyRepo.Query().Any(x => x.Id != companyDto.Id && x.ISIN == companyDto.Isin))
            {
                throw new ArgumentException("Company with new Isin already exists");
            }
            var initialRecord = _companyRepo.Query().Where(x => x.Id == companyDto.Id).FirstOrDefault();
            initialRecord.Exchange = companyDto.Exchange;
            initialRecord.ISIN = companyDto.Isin;
            initialRecord.Name = companyDto.Name;
            initialRecord.StockTicker = companyDto.StockTicker;
            initialRecord.Website = companyDto.Website;
            _companyRepo.Update(initialRecord);
            await _unitOfWork.Commit();
        }
    }
}
