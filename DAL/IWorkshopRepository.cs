using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface IWorkshopRepository
    {
        Task<IEnumerable<WorkshopModel>> Get();
        Task<WorkshopModel> GetById(int id);
        Task<IEnumerable<WorkshopModel>> GetByIds(IEnumerable<int> ids);
        Task Create(WorkshopModel model);
        Task Update(WorkshopModel model);
        Task Delete(int id);
    }
}