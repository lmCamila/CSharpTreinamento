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

        public bool Update(TypePlan type)
        {
            dt.ClearParams();
            string SQL = @"UPDATE  type_plans
                           SET name = @NAME, description = @DESCRIPTION
                           WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, type.Id);
            dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, type.Name);
            dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, type.Description);
            return (dt.ExecuteUpdate(SQL) > 0);
        }
        public bool Delete(int id)
        {
            dt.ClearParams();
            string SQL = @"DELETE FROM type_plan WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            return (dt.ExecuteUpdate(SQL) > 0);
        }
        public TypePlan GetForId(int id)
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM type_plan WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            DataTable dtResult = dt.ExecuteQuery(SQL);
            TypePlan tp = new TypePlan();
            tp.Id = Convert.ToInt32(dtResult.Rows[0]["id"].ToString());
            tp.Name = dtResult.Rows[0]["name"].ToString();
            tp.Description = dtResult.Rows[0]["description"].ToString();
            return tp;
        }
        public List<TypePlan> getAll()
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM type_plan";
            DataTable dtResult = dt.ExecuteQuery(SQL);
            var list = dtResult.DataSet.Tables[0].AsEnumerable()
                .Select(dataRow => new TypePlan {
                    Id = dataRow.Field<int>("id"),
                    Name = dataRow.Field<string>("name"),
                    Description = dataRow.Field<string>("description") })
                .ToList();
            return list;
        }
    }
}
