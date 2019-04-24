using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner
{
    class GenericDataBase
    {
        public static DbCommand CreateCommand (string cmdText, CommandType cmmType, List<DbParameter> listParameters)
        {
            var factory = DbProviderFactories.GetFactory(ConnectionDB.providerName);

            var conn = factory.CreateConnection();
            conn.ConnectionString = ConnectionDB.connectionString;
            var comm = conn.CreateCommand();
            comm.CommandText = cmdText;
            comm.CommandType = cmmType;
            if(listParameters != null)
            {
                foreach (var param in listParameters)
                {
                    comm.Parameters.Add(param);
                }
            }
            return comm;
        }

        public static DbParameter CreateParameter( string nameParameter, DbType typeParameters, Object valueParameter)
        {
            var factory = DbProviderFactories.GetFactory(ConnectionDB.providerName);

            var param = factory.CreateParameter();
            if(param != null)
            {
                param.ParameterName = nameParameter;
                param.DbType = typeParameters;
                param.Value = valueParameter;
            }
            return param;
        }

        public static Object ExecuteCommand(string cmdText, CommandType cmdType, List<DbParameter> listParameter, TypeCommand typeCmd)
        {
            var command = CreateCommand(cmdText, cmdType, listParameter);
            Object objReturn = null;
            try
            {
                command.Connection.Open();
                switch (typeCmd)
                {
                    case TypeCommand.ExecuteNonQuery:
                        objReturn = command.ExecuteNonQuery();
                        break;
                    case TypeCommand.ExecuteReader:
                        objReturn = command.ExecuteReader();
                        break;
                    case TypeCommand.ExecuteScalar:
                        objReturn = command.ExecuteScalar();
                        break;
                    case TypeCommand.ExecuteDataTable:
                        var table = new DataTable();
                        var reader = command.ExecuteReader();
                        table.Load(reader);
                        reader.Close();
                        objReturn = table;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro ao executar Execute Command");
            }
            finally
            {
                if(typeCmd != TypeCommand.ExecuteReader)
                {
                    if(command.Connection.State == ConnectionState.Open)
                    {
                        command.Connection.Close();
                    }
                    command.Dispose();
                }
            }
            return objReturn;
        }

        
    }
    public enum TypeCommand
    {
        ExecuteNonQuery,
        ExecuteReader,
        ExecuteScalar,
        ExecuteDataTable
    }
}
