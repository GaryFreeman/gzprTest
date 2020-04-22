using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;


namespace gzpr.Models.Repos
{
    public class WorkshopRepository
    {
        public List<WorkshopModel> Get() {
            using (SQLiteConnection conn = BaseRepository.Connection()) {
                string query = @"SELECT * FROM workshops;";
                conn.Open();
                
                return conn.Query<WorkshopModel>(query).ToList();
            }
        }

        public object GetById(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"SELECT * FROM workshops WHERE id=@id;";
                conn.Open();

                return conn.Query(query, new { id}).FirstOrDefault();
            }
        }

        public void Create(WorkshopModel model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"INSERT INTO workshops('name') VALUES (@name);";
                conn.Open();

                var result = conn.Query(query, model);
            }
        }

        public void Update(WorkshopModel model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"UPDATE workshops SET name=@name WHERE id=@id;";
                conn.Open();

                conn.Query(query, model);
            }
        }

        public void Delete(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"DELETE FROM workshops WHERE id=@id;";
                conn.Open();

                conn.Query(query, new { id });
            }
        }
    }
}
