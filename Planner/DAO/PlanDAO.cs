using Planner.Entity;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Planner.DAO
{
    class PlanDAO: IDataAccess
    {
        public bool insert(Plan plan,int idType, int idSponsor, string idsInterested)
        {
            int results = 0;
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int id = db.Execute("sp_insert_plans", new
                { Name = plan.Name, IdSponsor = idSponsor, IdType = idType,
                    Description = plan.Description, Costs = plan.Cost, StartDate = plan.Start,
                    EndDate = plan.End },commandType: CommandType.StoredProcedure);
                if(idsInterested != null)
                {
                    var ids = idsInterested.Split(',');
                    foreach (var item in ids)
                    {
                        results += db.Execute(@"INSERT INTO plans_interested_users (id_plan,id_user)
                                                    VALUES(@IdPlan, @IdUser)", new { IdPlan = id, IdUser = item });
                    }
                    if(results == ids.Length)
                    {
                        return true;
                    }
                }
                return (id > 0);
            }
            //dt.ClearParams();
            //string SQL = @"INSERT INTO plans(name,id_sponsor,id_type_plan,description,cost,start_date,end_date)
            //               VALUES(@NAME,@ID_SPONSOR,@ID_TYPE_PLAN,@DESCRIPTION,@COST,@START_DATE,@END_DATE)";
            //dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, plan.Name);
            //dt.AddParam("@ID_SPONSOR", System.Data.SqlDbType.Int, plan.Sponsor.Id);
            //dt.AddParam("@ID_TYPE_PLAN", System.Data.SqlDbType.Int, plan.Type.Id);
            //dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, plan.Description);
            //dt.AddParam("@START_DATE", System.Data.SqlDbType.DateTime, plan.Start);
            //dt.AddParam("@END_DATE", System.Data.SqlDbType.DateTime, plan.End);
            //return (dt.ExecuteUpdate(SQL) > 0);
        }
        public bool Update(Plan plan, int idType, int idSponsor, string idsInterested)
        {
            return false;
            //dt.ClearParams();
            //string SQL = @"UPDATE plans SET
            //                      name = @NAME,
            //                      id_sponsor = @ID_SPONSOR,
            //                      id_type_plan = @ID_TYPE_PLAN,
            //                      description = @DESCRIPTION,
            //                      cost = @COST,
            //                      start_date = @START_DATE,
            //                      end_date = @END_DATE
            //                WHERE id = @ID";
            //dt.AddParam("@ID", System.Data.SqlDbType.Int, plan.Id);
            //dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, plan.Name);
            //dt.AddParam("@ID_SPONSOR", System.Data.SqlDbType.Int, plan.Sponsor.Id);
            //dt.AddParam("@ID_TYPE_PLAN", System.Data.SqlDbType.Int, plan.Type.Id);
            //dt.AddParam("@DESCRIPTION", System.Data.SqlDbType.VarChar, plan.Description);
            //dt.AddParam("@START_DATE", System.Data.SqlDbType.DateTime, plan.Start);
            //dt.AddParam("@END_DATE", System.Data.SqlDbType.DateTime, plan.End);
            //return (dt.ExecuteUpdate(SQL) > 0);
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute("DELETE FROM plan WHERE id = @Id", new { Id = id },
                                        commandType: CommandType.Text);
                return (result > 0);
            }
            //dt.ClearParams();
            //string SQL = @"DELETE FROM plan WHERE id = @ID";
            //dt.AddParam("@ID",System.Data.SqlDbType.Int,id);
            //return (dt.ExecuteUpdate(SQL) > 0);
        }
        //public Plan GetById(int id)
        //{
        //    //name,id_sponsor,id_type_plan,description,cost,start_date,end_date
        //    dt.ClearParams();
        //    string SQL = @"SELECT * FROM plans WHERE id = @ID";
        //    dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
        //    DataTable dataTable = dt.ExecuteQuery(SQL);
        //    Plan p = new Plan();
        //    p.Id = Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
        //    p.Name = dataTable.Rows[0]["name"].ToString();
        //    //pegar typo e usuario antes 

        //}

        //public List<Plan> GetAll()
        //{

        //}
    }
}
