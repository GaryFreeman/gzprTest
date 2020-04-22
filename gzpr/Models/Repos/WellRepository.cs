using Dapper;
using gzpr.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace gzpr.Models.Repos
{
    public class WellRepository
    {
        public List<WellModel> Get() 
        {
            using (SQLiteConnection conn = BaseRepository.Connection()) {
                string query = @"SELECT wells.*, companies.*, workshops.*, fields.* FROM wells 
                                                                            INNER JOIN companies ON company_id == companies.id 
                                                                            INNER JOIN workshops ON workshop_id == workshops.id 
                                                                            INNER JOIN fields ON field_id == fields.id;";
                conn.Open();
                var wells = conn.Query<WellModel, CompanyModel, WorkshopModel, FieldModel,  WellModel>(query, (well, company, workshop, field) =>
                {
                    well.Company = company;
                    well.Field = field;
                    well.Workshop = workshop;
                    return well;
                });
                return wells.ToList();
            }
        }

        public object GetById(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"SELECT wells.*, companies.*, workshops.*, fields.* FROM wells 
                                                                            INNER JOIN companies ON company_id == companies.id 
                                                                            INNER JOIN workshops ON workshop_id == workshops.id 
                                                                            INNER JOIN fields ON filed_id == fields.id
                                                                            WHERE id = @id;";
                conn.Open();
                var well = conn.Query<WellModel, CompanyModel, WorkshopModel, FieldModel, WellModel>(query, (well, company, workshop, field) => {
                    well.Company = company;
                    well.Workshop = workshop;
                    well.Field = field;
                    return well;
                }).FirstOrDefault();
                return well;
            }
        }

        public void Create(WellRequest model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"INSERT INTO wells (name, company_id, field_id, workshop_id) VALUES (@name, @company_id, @field_id, @workshop_id);";
                conn.Open();

                var result = conn.Query(query, model);
            }
        }

        public void Update(WellRequest model)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                //string query = @"UPDATE wells SET name=@name, company_id=@company_id, workshop_id=@workshop_id, field_id=@field_id WHERE id=@id;";
                string query = @"UPDATE wells SET ";
                conn.Open();

                foreach (PropertyInfo prop in model.GetType().GetProperties()) {
                    if (prop.Name == "id") continue;
                    if (prop.GetValue(model, null) != null) {
                        query += String.Format("{0}={1} ", prop.Name, prop.GetValue(model, null));
                    }
                }
                query += "WHERE id=" + model.id;
                var result = conn.Query(query);
            }
        }

        public void Delete(int id)
        {
            using (SQLiteConnection conn = BaseRepository.Connection())
            {
                string query = @"DELETE FROM wells WHERE id=@id;";
                conn.Open();

                conn.Query(query, new { id });
            }
        }
    }
}
