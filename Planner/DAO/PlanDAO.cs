using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.DAO
{
    class PlanDAO
    {
        private DataAccess dt;
        public PlanDAO()
        {
            dt = new DataAccess();
        }
        //pensar sobre id em vez de objeto em tipo e sponsor
        public bool insert(Plan plan)
        {
            dt.ClearParams();
            string SQL = @"INSERT INTO plans(name,id_sponsor,id_type_plan,description,cost,start_date,end_date)
                           VALUES(@NAME,@ID_SPONSOR,@ID_TYPE_PLAN,@DESCRIPTION,@COST,@START_DATE,@END_DATE)";
            dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, plan.Name);
            dt.AddParam("@ID_SPONSOR", System.Data.SqlDbType.Int, plan.Sponsor.Id);
            dt.AddParam("@ID_TYPE_PLAN", System.Data.SqlDbType.Int, plan.Type.Id);
            dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, plan.Description);
            dt.AddParam("@START_DATE", System.Data.SqlDbType.DateTime, plan.Start);
            dt.AddParam("@END_DATE", System.Data.SqlDbType.DateTime, plan.End);
            return (dt.ExecuteUpdate(SQL) > 0);
        }
        public bool Update(Plan plan)
        {
            dt.ClearParams();
            string SQL = @"UPDATE plans SET
                                  name = @NAME,
                                  id_sponsor = @ID_SPONSOR,
                                  id_type_plan = @ID_TYPE_PLAN,
                                  description = @DESCRIPTION,
                                  cost = @COST,
                                  start_date = @START_DATE,
                                  end_date = @END_DATE
                            WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, plan.Id);
            dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, plan.Name);
            dt.AddParam("@ID_SPONSOR", System.Data.SqlDbType.Int, plan.Sponsor.Id);
            dt.AddParam("@ID_TYPE_PLAN", System.Data.SqlDbType.Int, plan.Type.Id);
            dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, plan.Description);
            dt.AddParam("@START_DATE", System.Data.SqlDbType.DateTime, plan.Start);
            dt.AddParam("@END_DATE", System.Data.SqlDbType.DateTime, plan.End);
            return (dt.ExecuteUpdate(SQL) > 0);
        }

        public bool Delete(int id)
        {
            dt.ClearParams();
            string SQL = @"DELETE FROM plan WHERE id = @ID";
            dt.AddParam("@ID",System.Data.SqlDbType.Int,id);
            return (dt.ExecuteUpdate(SQL) > 0);
        }
        public Plan GetById(int id)
        {
            //name,id_sponsor,id_type_plan,description,cost,start_date,end_date
            dt.ClearParams();
            string SQL = @"SELECT * FROM plans WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            DataTable dataTable = dt.ExecuteQuery(SQL);
            Plan p = new Plan();
            p.Id = Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
            p.Name = dataTable.Rows[0]["name"].ToString();
            //pegar typo e usuario antes 

        }
        public List<Plan> GetAll()
        {

        }
    }
}
