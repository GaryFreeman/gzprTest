using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace BusinessLogic
{
    public interface IWellRequestHandler
    {
        Task<IEnumerable<WellModel>> Get();
        Task<WellModel> Get(int id);
        Task Create(WellModel well);
        Task Update(WellModel model);
        Task Delete(int id);
    }
}