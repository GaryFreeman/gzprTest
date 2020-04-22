using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace DAL
{
    public class FieldRepository : IFieldRepository
    {
        public async Task<IEnumerable<FieldModel>> Get()
        {
            const string query = @"SELECT * FROM fields;";
            
            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            return await conn.QueryAsync<FieldModel>(query);
        }

        public async Task<FieldModel> GetById(int id)
        {
            const string query = @"SELECT * FROM fields WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            return await conn.QueryFirstOrDefaultAsync<FieldModel>(query, new { id });
        }
        
        public async Task<IEnumerable<FieldModel>> GetByIds(IEnumerable<int> ids)
        {
            const string query = @"SELECT * FROM fields WHERE id IN @ids";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            return await conn.QueryAsync<FieldModel>(query, new { ids });
        }

        public async Task Create(FieldModel model)
        {
            const string query = @"INSERT INTO fields ('name') VALUES (@name);";
            
            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, model);
        }

        public async Task Update(FieldModel model)
        {
            const string query = @"UPDATE fields SET name=@name WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, model);
        }

        public async Task Delete(int id)
        {
            const string query = @"DELETE FROM fields WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, new { id });
        }
    }
}
