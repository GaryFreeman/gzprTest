using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;


namespace gzpr.Models.Repos
{
    public class FieldRepository
    {
        public List<FieldModel> Get() {
            using (SQLiteConnection conn = BaseRepository.Connection()) {
                string query = @"SELECT * FROM fields;";
                conn.Open();
                
                return conn.Query<FieldModel>(query).ToList();
            }
        }

        public object GetById(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"SELECT * FROM fields WHERE id=@id;";
                conn.Open();

                return conn.Query(query, new { id}).FirstOrDefault();
            }
        }

        public void Create(FieldModel model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"INSERT INTO fields ('name') VALUES (@name);";
                conn.Open();

                var result = conn.Query(query, model);
            }
        }

        public void Update(FieldModel model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"UPDATE fields SET name=@name WHERE id=@id;";
                conn.Open();

                conn.Query(query, model);
            }
        }

        public void Delete(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"DELETE FROM fields WHERE id=@id;";
                conn.Open();

                conn.Query(query, new { id });
            }
        }
    }
}
