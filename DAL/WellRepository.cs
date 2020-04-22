using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace DAL
{
    public class WellRepository : IWellRepository
    {
        public async Task<IEnumerable<WellModel>> Get()
        {
            const string query = @"SELECT * FROM wells 
                                   INNER JOIN companies ON company_id == companies.id 
                                   INNER JOIN workshops ON workshop_id == workshops.id 
                                   INNER JOIN fields ON field_id == fields.id;";

            await using var conn = BaseRepository.Connection();

            await conn.OpenAsync();
            var wells =
                await conn.QueryAsync<WellModel, CompanyModel, WorkshopModel, FieldModel, WellModel>(
                    query,
                    (well, company, workshop, field) =>
                    {
                        well.Company = company;
                        well.Field = field;
                        well.Workshop = workshop;
                        return well;
                    });

            return wells;
        }

        public async Task<WellModel> GetById(int id)
        {
            const string query = @"SELECT * FROM wells 
                                   INNER JOIN companies ON company_id == companies.id 
                                   INNER JOIN workshops ON workshop_id == workshops.id 
                                   INNER JOIN fields ON field_id == fields.id
                                   WHERE wells.id = @id;";

            await using var conn = BaseRepository.Connection();
            
            await conn.OpenAsync();
            var wells =
                await conn.QueryAsync<WellModel, CompanyModel, WorkshopModel, FieldModel, WellModel>(
                    query,
                    (well, company, workshop, field) =>
                    {
                        well.Company = company;
                        well.Workshop = workshop;
                        well.Field = field;
                        return well;
                    });

            return wells.FirstOrDefault();
        }

        public async Task Create(WellModel model)
        {
            const string query = @"INSERT INTO wells (name, company_id, field_id, workshop_id) VALUES (@name, @company_id, @field_id, @workshop_id);";
            
            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, model);
        }

        public async Task Update(WellModel model)
        {
            var query = new StringBuilder("UPDATE wells SET");

            foreach (var prop in model.GetType().GetProperties())
            {
                if (prop.Name == nameof(model.Id)) continue;
                if (prop.GetValue(model, null) != null)
                {
                    query.Append($", {prop.Name} = @{prop.Name}");
                }
            }
            query.Append("WHERE id = @Id");
            query.Replace("SET, ", "SET ");

            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query.ToString(), model);
        }

        public async Task Delete(int id)
        {
            const string query = @"DELETE FROM wells WHERE id=@id;";
            
            await using var conn = BaseRepository.Connection();
            await conn.OpenAsync();

            await conn.QueryAsync(query, new { id });
        }
    }
}
