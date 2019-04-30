using Dapper;
using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Planner.DAO
{
    class TypePlanDAO : DataAccess
    {
        
        public bool Insert(TypePlan type)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute(@"INSERT INTO type_plans(name,description)
                            VALUES(@NAME, @DESCRIPTION)",type);
                return (result > 0);
            }
            
        }

        public bool Update(Dictionary<string, string> typeDictionary, string att)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                int result;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                if ("desc".Equals(att))
                {
                    string SQL = @"UPDATE  type_plans 
                           SET description = @Description 
                           WHERE id = @Id";
                    result = db.Execute(SQL, new { Id = Convert.ToInt32(typeDictionary["id"]),
                                                   Description = typeDictionary["desc"] });
                }
                else if ("name".Equals(att))
                {
                    string SQL = @"UPDATE  type_plans 
                           SET name = @Name 
                           WHERE id = @Id";
                    result = db.Execute(SQL, new
                    {
                        Id = Convert.ToInt32(typeDictionary["id"]),
                        Name = typeDictionary["name"]
                    });
                }
                else
                {
                    string SQL = @"UPDATE  type_plans 
                           SET name = @Name, description = @Description
                           WHERE id = @Id";
                    result = db.Execute(SQL, new
                    {
                        Id = Convert.ToInt32(typeDictionary["id"]),
                        Name = typeDictionary["name"],
                        Description = typeDictionary["desc"]
                    });

                }
                return (result > 0);
            }
        }
        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute("DELETE FROM type_plans WHERE id = @Id", new { Id = id }, commandType: CommandType.Text);
                return (result > 0);
            }
        }
        public TypePlan GetForId(int id)
        {
            TypePlan type = new TypePlan();
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                type = db.QueryFirst<TypePlan>("SELECT * FROM type_plans WHERE id = @Id", new { Id = id });
            }
                return type;
        }

        public TypePlan GetForName(string name)
        {
            TypePlan type = new TypePlan();
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                type = db.QueryFirst<TypePlan>("SELECT * FROM type_plans WHERE name LIKE '%@Name%'", 
                                                new { Name = name });
                return type;
            }
        }

        public List<TypePlan> getAll()
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                IEnumerable<TypePlan> types = db.Query<TypePlan>("SELECT * FROM type_plans");
                return (List<TypePlan>)types;
            }
        }
    }
}
