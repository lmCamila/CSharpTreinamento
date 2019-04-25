using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner
{
    class DataAccess
    {
        public static SqlConnection sqlConnection = new SqlConnection();
        public static SqlCommand command = new SqlCommand();
        public static SqlParameter param = new SqlParameter();

        public static SqlConnection connection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                return sqlConnection;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao gerar/pegar conexão.Mensagem:{ex.Message}");
            }
        }
        public void Open()
        {
            sqlConnection.Open();
        }
        public void Close()
        {
            sqlConnection.Close();
        }
        public void AddParam(string name, SqlDbType type,int size, object value)
        {
            param = new SqlParameter();
            param.ParameterName = name;
            param.SqlDbType = type;
            param.Size = size;
            param.Value = value;
            command.Parameters.Add(param);
        }
        public void AddParam(string name, SqlDbType type, object value)
        {
            param = new SqlParameter();
            param.ParameterName = name;
            param.SqlDbType = type;
            param.Value = value;
            command.Parameters.Add(param);
        }
        public void RemoveParam(string name)
        {
            if (command.Parameters.Contains(name))
            {
                command.Parameters.Remove(name);
            }
        }
        public void ClearParams()
        {
            command.Parameters.Clear();
        }

        public DataTable ExecuteQuery(string sql)
        {
            try
            {
                command.Connection = connection();
                command.CommandText = sql;
                command.ExecuteScalar();
                IDataReader dtreader = command.ExecuteReader();
                DataTable dtresult = new DataTable();
                dtresult.Load(dtreader);
                sqlConnection.Close();
                return dtresult;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao executar query. Mensagem: {ex.Message}");
            }
        }
        public int ExecuteUpdate(string sql)
        {
            try
            {
                //command = new SqlCommand(sql, connection());
                command.Connection = connection();
                command.CommandText = sql;
                int result = command.ExecuteNonQuery();
                sqlConnection.Close();
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao executar update|insert|delete. Mensagem:{ex.Message}");
            }
        }
    }
}
