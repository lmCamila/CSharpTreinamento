using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.DAO
{
    class TypePlanDAO
    {
        DataAccess dt;
        public TypePlanDAO()
        {
            dt = new DataAccess();
        }
        public bool Insert(TypePlan type)
        {
            dt.ClearParams();
            string SQL = @"INSERT INTO type_plans(name,description)
                            VALUES(@NAME, @DESCRIPTION)";
            dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, type.Name);
            dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, type.Description);
            return (dt.ExecuteUpdate(SQL) > 0);
        }

        public bool Update(Dictionary<string, string> typeDictionary, string att)
        {
            dt.ClearParams();
            string set = null;
            if ("desc".Equals(att))
            {
                set = "description = @DESCRIPTION";
                dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, typeDictionary["desc"]);
            }                
            else if ("name".Equals(att))
            {
                set = "name = @NAME";
                dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, typeDictionary["name"]);
            }
            else
            {
                set = "name = @NAME, description = @DESCRIPTION";
                dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, typeDictionary["name"]);
                dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, typeDictionary["desc"]);
            }
            string SQL = $@"UPDATE  type_plans 
                           SET {set} 
                           WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, Convert.ToInt32(typeDictionary["id"]));
           
            return (dt.ExecuteUpdate(SQL) > 0);
        }
        public bool Delete(int id)
        {
            dt.ClearParams();
            string SQL = @"DELETE FROM type_plans WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            return (dt.ExecuteUpdate(SQL) > 0);
        }
        public TypePlan GetForId(int id)
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM type_plans WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            DataTable dtResult = dt.ExecuteQuery(SQL);
            TypePlan type = new TypePlan();
            type.Id = Convert.ToInt32(dtResult.Rows[0]["id"].ToString());
            type.Name = dtResult.Rows[0]["name"].ToString();
            type.Description = dtResult.Rows[0]["description"].ToString();
            return type;
        }

        public TypePlan GetForName(string name)
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM type_plans WHERE name LIKE '%@NAME%'";
            dt.AddParam("@NAME", System.Data.SqlDbType.Int, name);
            DataTable dtResult = dt.ExecuteQuery(SQL);
            TypePlan type = new TypePlan();
            type.Id = Convert.ToInt32(dtResult.Rows[0]["id"].ToString());
            type.Name = dtResult.Rows[0]["name"].ToString();
            type.Description = dtResult.Rows[0]["description"].ToString();
            return type;
        }

        public List<TypePlan> getAll()
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM type_plans";
            DataTable dtResult = dt.ExecuteQuery(SQL);
            var list = dtResult.AsEnumerable()
                .Select(dataRow => new TypePlan {
                    Id = dataRow.Field<int>("id"),
                    Name = dataRow.Field<string>("name"),
                    Description = dataRow.Field<string>("description") })
                .ToList();
            return list;
        }
    }
}
