using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;


namespace gzpr.Models.Repos
{
    public class CompanyRepository
    {

        public List<CompanyModel> Get() {
            using (SQLiteConnection conn = BaseRepository.Connection()) {
                string query = @"SELECT * FROM companies;";
                conn.Open();
                
                return conn.Query<CompanyModel>(query).ToList();
            }
        }

        public object GetById(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"SELECT * FROM companies WHERE id=@id;";
                conn.Open();

                return conn.Query(query, new { id}).FirstOrDefault();
            }
        }

        public void Create(CompanyModel model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"INSERT INTO companies ('name') VALUES (@name);";
                conn.Open();

                var result = conn.Query(query, model);
            }
        }

        public void Update(CompanyModel model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"UPDATE companies SET name=@name WHERE id=@id;";
                conn.Open();

                conn.Query(query, model);
            }
        }

        public void Delete(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"DELETE FROM companies WHERE id=@id;";
                conn.Open();

                conn.Query(query, new { id });
            }
        }
    }
}
