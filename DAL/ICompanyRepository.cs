using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyModel>> Get();
        Task<CompanyModel> GetById(int id);
        Task<IEnumerable<CompanyModel>> GetByIds(IEnumerable<int> ids);
        Task Create(CompanyModel model);
        Task Update(CompanyModel model);
        Task Delete(int id);
    }
}