using Planner.Entity;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace Planner.DAO
{
    class PlanDAO: DataAccess
    {
        public bool insert(Plan plan, string idsInterested)
        {
            int results = 0;
           
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                results = db.Execute("sp_insert_plans", new
                {
                    Name = plan.Name,
                    IdSponsor = plan.Sponsor.Id,
                    IdType = plan.Type.Id,
                    InterestedUser = idsInterested,
                    Description = plan.Description,
                    Costs = plan.Cost,
                    StartDate = plan.Start,
                    EndDate = plan.End
                }, commandType: CommandType.StoredProcedure);


                return (results > 0);
            }
        }
        public bool Update(Dictionary<string, string> plan)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                string SQL = @"UPDATE plans SET
                                  name = @Name,
                                  id_sponsor = @IdSponsor,
                                  id_type_plan = @IdTypePlan,
                                  description = @Description,
                                  cost = @Cost,
                                  start_date = @StartDate,
                                  end_date = @EndDate
                            WHERE id = @Id";
                int result = db.Execute(SQL, new { Name = plan["name"], IdSponsor = plan["idSponsor"], IdTypePlan = plan["idType"],
                    Description = plan["description"], Cost=plan["cost"], StartDate=plan["start"], EndDate = plan["end"],
                    Id = plan["id"]
                });
                if (plan["idsInterested"] != null)
                {
                    result += db.Execute("DELETE FROM plan WHERE idPlan = @Id", new { Id = plan["id"] },
                                        commandType: CommandType.Text);
                    var ids = plan["idsInterested"].Split(',');
                    foreach (var item in ids)
                    {
                        result += db.Execute(@"INSERT INTO plans_interested_users (id_plan,id_user)
                                              VALUES(@IdPlan, @IdUser)", new { IdPlan = plan["id"], IdUser = item });
                    }
                    
                }
                return (result > 0);
            }
           
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
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute("DELETE FROM plans WHERE id = @Id", new { Id = id },
                                        commandType: CommandType.Text);
                return (result > 0);
            }
            //dt.ClearParams();
            //string SQL = @"DELETE FROM plan WHERE id = @ID";
            //dt.AddParam("@ID",System.Data.SqlDbType.Int,id);
            //return (dt.ExecuteUpdate(SQL) > 0);
        }

        public Plan GetById(int id)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Close();
                }

                var query = db.Query<Plan, User, TypePlan, Plan>(@"SELECT p.* , tp.*, u.*
                        FROM plans p inner join type_plans tp on p.id_type_plan = tp.id
                        inner join users u on p.id_sponsor = u.id
                        where p.id = @Id"
                , (p, u, tp) =>
                {
                    p.Sponsor = u;
                    p.Type = tp;
                    return p;
                }, new { Id = id }, splitOn: "id,id,id"
                ).AsList();
               
                return query[0];
            }
            //name,id_sponsor,id_type_plan,description,cost,start_date,end_date
            //dt.ClearParams();
            //string SQL = @"SELECT * FROM plans WHERE id = @ID";
            //dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            //DataTable dataTable = dt.ExecuteQuery(SQL);
            //Plan p = new Plan();
            //p.Id = Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
            //p.Name = dataTable.Rows[0]["name"].ToString();
            //pegar typo e usuario antes 

        }

        public List<Plan> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Close();
                }

                var query = db.Query<Plan, User, TypePlan, Plan>(@"SELECT p.* , tp.*, u.*
                        FROM plans p inner join type_plans tp on p.id_type_plan = tp.id
                        inner join users u on p.id_sponsor = u.id"
                , (p, u, tp) =>
                {
                    p.Sponsor = u;
                    p.Type = tp;
                    return p;
                }, null, splitOn: "id,id,id"
                ).AsList();
                return query;
            }
        }
    }
}
