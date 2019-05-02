using System;
using System.Collections.Generic;

namespace Planner.Utils
{
    class GenerateDictionaries
    {
        public static Dictionary<string, string> generateTypeDictionary(int id, string name, string desc)
        {
            Dictionary<string, string> generic = new Dictionary<string, string>();
            generic.Add("id", Convert.ToString(id));
            generic.Add("name", name);
            generic.Add("desc", desc);
            return generic;
        }
        public static  Dictionary<string, string> generateUserDictionary(int id, string name, string email)
        {
            Dictionary<string, string> generic = new Dictionary<string, string>();
            generic.Add("id", Convert.ToString(id));
            generic.Add("name", name);
            generic.Add("email", email);
            return generic;
        }
        public static Dictionary<string, string> generatePlanDictionary(int id, string name, long idSponsor,
            long idType, string idsInterested = null, string description = null, double costs = 0,
            string startDate = null, string endDate = null)
        {
            Dictionary<string, string> generic = new Dictionary<string, string>();
            generic.Add("id", Convert.ToString(id));
            generic.Add("name", name);
            generic.Add("idSponsor", Convert.ToString(idSponsor));
            generic.Add("idType", Convert.ToString(idType));
            generic.Add("idsInterested", idsInterested);
            generic.Add("description", description);
            generic.Add("costs", Convert.ToString(costs));
            generic.Add("start", startDate);
            generic.Add("end", endDate);
            return generic;
        }
        public static Dictionary<string, string> generatePlanDictionary(int id, string name, string idSponsor,
           string idType, string idsInterested = null, string description = null, string costs = null,
           string startDate = null, string endDate = null)
        {
            Dictionary<string, string> generic = new Dictionary<string, string>();
            generic.Add("id", Convert.ToString(id));
            generic.Add("name", name);
            generic.Add("idSponsor", Convert.ToString(idSponsor));
            generic.Add("idType", Convert.ToString(idType));
            generic.Add("idsInterested", idsInterested);
            generic.Add("description", description);
            generic.Add("costs", Convert.ToString(costs));
            generic.Add("start", startDate);
            generic.Add("end", endDate);
            return generic;
        }
    }
}
