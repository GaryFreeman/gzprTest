using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BusinessLogic
{
    public class CompanyRequestHandler : ICompanyRequestHandler
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyRequestHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<IEnumerable<CompanyModel>> Get()
        {
            return _companyRepository.Get();
        }

        public Task<CompanyModel> Get(int id)
        {
            return _companyRepository.GetById(id);
        }
        
        public Task Create(CompanyModel company)
        {
            return _companyRepository.Create(company);
        }
        
        public Task Update(CompanyModel company)
        {
            return _companyRepository.Update(company);
        }
        
        public Task Delete(int id)
        {
            return _companyRepository.Delete(id);
        }
    }
}