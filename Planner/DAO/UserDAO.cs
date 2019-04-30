using Dapper;
using Planner.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Planner.DAO
{
    class UserDAO : DataAccess
    {
        public bool Insert(User user)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString)) 
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute(@"INSERT INTO users (name, email)
                                            VALUES(@Name, @Email)", user);
                return (result > 0);
            }
        }

        public bool Update(Dictionary<string, string> userDictionary, string attr)
        {
            
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                int result;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                if ("name".Equals(attr))
                {
                    string SQL = @"UPDATE users SET name = @Name WHERE id = @Id";
                    result = db.Execute(SQL, new { Id = userDictionary["id"], Name = userDictionary["name"] });
                }
                else if ("email".Equals(attr))
                {
                    string SQL = @"UPDATE users SET email = @Email WHERE id = @Id";
                    result = db.Execute(SQL, new { Id = userDictionary["id"], Email = userDictionary["email"] });
                }
                else
                {
                    string SQL = @"UPDATE users SET name = @NAME, email = @EMAIL WHERE id = @Id";
                    result = db.Execute(SQL, new { Id = userDictionary["email"], Name = userDictionary["name"]
                                                    ,Email = userDictionary["email"]});
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
                int result = db.Execute("DELETE FROM users WHERE id = @Id", new { Id = id },
                                        commandType: CommandType.Text);
                return (result > 0);
            }
        }
        public User GetById(int id)
        {
            User user = new User();
            using(IDbConnection db = new SqlConnection(ConnectionString))
             {
                  if(db.State == ConnectionState.Closed)
                  {
                      db.Open();
                  }
                    user = db.QueryFirst<User>("SELECT * FROM users WHERE id = @Id", new { Id = id});
             }
            return user;
        }

        public User GetByName(string name)
        {
            User user = new User();
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                user = db.QueryFirst<User>("SELECT * FROM users WHERE name = LIKE '%@Name%'", new { Name = name });
            }
            return user;
        }

        public List<User> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                IEnumerable<User> users = db.Query<User>("SELECT * FROM users");
                return (List<User>)users;
            }
        }
    }
}
