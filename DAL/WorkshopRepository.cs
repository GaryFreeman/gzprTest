using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace DAL
{
    public class WorkshopRepository : IWorkshopRepository
    {
        public async Task<IEnumerable<WorkshopModel>> Get()
        {
            const string query = @"SELECT * FROM workshops;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();
                
            return await conn.QueryAsync<WorkshopModel>(query);
        }

        public async Task<WorkshopModel> GetById(int id)
        {
            const string query = @"SELECT * FROM workshops WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            return await conn.QueryFirstAsync<WorkshopModel>(query, new { id });
        }
        
        public async Task<IEnumerable<WorkshopModel>> GetByIds(IEnumerable<int> ids)
        {
            const string query = @"SELECT * FROM workshops WHERE id IN @ids";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            return await conn.QueryAsync<WorkshopModel>(query, new { ids });
        }

        public async Task Create(WorkshopModel model)
        {
            const string query = @"INSERT INTO workshops('name') VALUES (@name);";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            var result = conn.Query(query, model);
        }

        public async Task Update(WorkshopModel model)
        {
            const string query = @"UPDATE workshops SET name=@name WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, model);
        }

        public async Task Delete(int id)
        {
            const string query = @"DELETE FROM workshops WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, new { id });
        }
    }
}
