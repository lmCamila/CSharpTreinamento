using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.DAO
{
    class UserDAO
    {
        DataAccess dt;
        public UserDAO()
        {
            dt = new DataAccess();
        }
        public bool Insert(User user)
        {
            dt.ClearParams();
            string SQL = @"INSERT INTO users (name, email)
                           VALUES(@NAME, @EMAIL)";
            dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, user.Name);
            dt.AddParam("@EMAIL", System.Data.SqlDbType.VarChar, user.Email);
            return (dt.ExecuteUpdate(SQL) > 0);
        }

        public bool Update(User user)
        {
            dt.ClearParams();
            string SQL = @"UPDATE user 
                           SET name = @NAME, email = @EMAIL
                           WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, user.Id);
            dt.AddParam("@NAME", System.Data.SqlDbType.VarChar, user.Name);
            dt.AddParam("@EMAIL", System.Data.SqlDbType.VarChar, user.Email);
            return (dt.ExecuteUpdate(SQL) > 0);
        }

        public bool Delete(int id)
        {
            dt.ClearParams();
            string SQL = @"DELETE FROM users WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            return (dt.ExecuteUpdate(SQL) > 0);
        }

        public User GetById(int id)
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM users WHERE id = @ID";
            dt.AddParam("@ID", System.Data.SqlDbType.Int, id);
            DataTable dtResult = dt.ExecuteQuery(SQL);
            User user = new User();
            user.Id = Convert.ToInt32(dtResult.Rows[0]["id"].ToString());
            user.Name = dtResult.Rows[0]["name"].ToString();
            user.Email = dtResult.Rows[0]["email"].ToString();
            return user;
        }

        public User GetByName(string name)
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM users WHERE name LIKE '%@NAME%'";
            dt.AddParam("@NAME", SqlDbType.VarChar, name);
            DataTable dtResult = dt.ExecuteQuery(SQL);
            User user = new User();
            user.Id = Convert.ToInt32(dtResult.Rows[0]["id"].ToString());
            user.Name = dtResult.Rows[0]["name"].ToString();
            user.Email = dtResult.Rows[0]["email"].ToString();
            return user;
        }

        public List<User> GetAll()
        {
            dt.ClearParams();
            string SQL = @"SELECT * FROM users";
            DataTable dtResult = dt.ExecuteQuery(SQL);
            var list = dtResult.DataSet.Tables[0].AsEnumerable()
                .Select(dataRow => new User
                {
                    Id = dataRow.Field<int>("id"),
                    Name = dataRow.Field<string>("name"),
                    Email = dataRow.Field<string>("email")
                })
                .ToList();
            return list;
        }
    }
}
