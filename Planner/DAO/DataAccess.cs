
using System.Configuration;


namespace Planner.DAO
{
    abstract class DataAccess
    {
        public static string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}
