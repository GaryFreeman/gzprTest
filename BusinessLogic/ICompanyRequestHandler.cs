using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace BusinessLogic
{
    public interface ICompanyRequestHandler
    {
        Task<IEnumerable<CompanyModel>> Get();
        Task<CompanyModel> Get(int id);
        Task Create(CompanyModel company);
        Task Update(CompanyModel company);
        Task Delete(int id);
    }
}