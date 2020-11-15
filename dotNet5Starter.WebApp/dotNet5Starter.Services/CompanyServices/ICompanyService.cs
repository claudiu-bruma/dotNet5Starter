using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNet5Starter.Services.CompanyServices
{
    public interface ICompanyService
    {
        int Add(CompanyDto newCompany);
        CompanyDto GetCompanyById(int id);
        CompanyDto GetCompanyByIsin(string Isin);
        IEnumerable<CompanyDto> GetCompanies();
        Task Update(CompanyDto companyDto);
    }
}
