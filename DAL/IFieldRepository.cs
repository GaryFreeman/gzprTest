using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface IFieldRepository
    {
        Task<IEnumerable<FieldModel>> Get();
        Task<FieldModel> GetById(int id);
        Task<IEnumerable<FieldModel>> GetByIds(IEnumerable<int> ids);
        Task Create(FieldModel model);
        Task Update(FieldModel model);
        Task Delete(int id);
    }
}