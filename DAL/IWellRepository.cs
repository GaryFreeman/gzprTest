using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface IWellRepository
    {
        Task<IEnumerable<WellModel>> Get();
        Task<WellModel> GetById(int id);
        Task Create(WellModel model);
        Task Update(WellModel model);
        Task Delete(int id);
    }
}