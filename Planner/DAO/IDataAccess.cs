
using System.Configuration;


namespace Planner.DAO
{
    class IDataAccess
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}
