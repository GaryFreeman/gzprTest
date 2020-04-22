using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace DAL
{
    public class CompanyRepository : ICompanyRepository
    {
        public async Task<IEnumerable<CompanyModel>> Get()
        {
            const string query = @"SELECT * FROM companies;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();
                
            return await conn.QueryAsync<CompanyModel>(query);
        }

        public async Task<CompanyModel> GetById(int id)
        {
            const string query = @"SELECT * FROM companies WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            return await conn.QueryFirstOrDefaultAsync<CompanyModel>(query, new { id });
        }

        public async Task<IEnumerable<CompanyModel>> GetByIds(IEnumerable<int> ids)
        {
            const string query = @"SELECT * FROM companies WHERE id IN @ids";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            return await conn.QueryAsync<CompanyModel>(query, new { ids });
        }

        public async Task Create(CompanyModel model)
        {
            const string query = @"INSERT INTO companies ('name') VALUES (@name);";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, model);
        }

        public async Task Update(CompanyModel model)
        {
            const string query = @"UPDATE companies SET name=@name WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, model);
        }

        public async Task Delete(int id)
        {
            const string query = @"DELETE FROM companies WHERE id=@id;";

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, new { id });
        }
    }
}
