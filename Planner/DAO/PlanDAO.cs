using Planner.Entity;
using System;
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
                    StartDate = plan.StartDate,
                    EndDate = plan.EndDate
                }, commandType: CommandType.StoredProcedure);


                return (results > 0);
            }
        }
        public bool Update(Dictionary<string, string> plan,string attr)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                DateTime ? start = null;
                DateTime  ? end = null;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                if(DateTime.Compare(Convert.ToDateTime(plan["start"]), DateTime.MinValue) > 0 &&
                    DateTime.Compare(Convert.ToDateTime(plan["start"]), DateTime.MaxValue) < 0)
                {
                   start = Convert.ToDateTime(plan["start"]);
                }
                if(DateTime.Compare(Convert.ToDateTime(plan["end"]), DateTime.MaxValue) < 0 
                    && DateTime.Compare(Convert.ToDateTime(plan["end"]), DateTime.MinValue) > 0)
                {
                    end = Convert.ToDateTime(plan["end"]);
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
                    Description = plan["description"], Cost=plan["costs"], StartDate = start,
                    EndDate = end,Id = plan["id"]
                });
                if (attr.Contains("idsInterested"))
                {
                    result += AlterInterested(plan["idsInterested"], Convert.ToInt32(plan["id"]));
                    
                }
                return (result > 0);
            }
        }
        private int AlterInterested(string idsInterested , int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var result = db.Execute("DELETE FROM plans_interested_users WHERE id_plan = @Id", new { Id = id },
                                       commandType: CommandType.Text);
                var ids = idsInterested.Split(',');
                foreach (var item in ids)
                {
                    result += db.Execute(@"INSERT INTO plans_interested_users (id_plan,id_user)
                                              VALUES(@IdPlan, @IdUser)", new { IdPlan = id, IdUser = item });
                }
                return result;
            }
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
        }

        public Plan GetById(int id)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Close();
                }

                var query = db.Query<Plan, User, TypePlan, Plan>(@"SELECT p.* , u.*, tp.*
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
                var queryInterested = db.Query<User>(@"SELECT u.*
                                                    FROM users u INNER JOIN plans_interested_users piu ON u.id = piu.id_user
                                                    WHERE id_plan = @Id ",new { Id = id });
                query[0].Interested = (List<User>)queryInterested;
                return query[0];
            }
            
        }

        public List<Plan> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Close();
                }

                var query = db.Query<Plan, User, TypePlan, Plan>(@"SELECT p.* , u.*, tp.*
                        FROM plans p inner join type_plans tp on p.id_type_plan = tp.id
                        inner join users u on p.id_sponsor = u.id"
                , (p, u, tp) =>
                {
                    p.Sponsor = u;
                    p.Type = tp;
                    return p;
                }, null, splitOn: "id,id,id"
                ).AsList();
                foreach (var item in query)
                {
                    var queryInterested = db.Query<User>(@"SELECT u.*
                                                    FROM users u INNER JOIN plans_interested_users piu ON u.id = piu.id_user
                                                    WHERE id_plan = @Id ", new { Id = item.Id });
                    item.Interested = (List<User>)queryInterested;
                }
                return query;
            }
        }
    }
}
