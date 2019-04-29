using Planner.DAO;
using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business
{
    class UserBusiness
    {
        UserDAO dao = new UserDAO();
        public bool Create(string name, string email)
        {
            if(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(email))
                throw new Exception("Nome ou email não pode ser nulo");

            User user = new User();
            user.Name = name;
            user.Email = email;
            return dao.Insert(user);
        }

        public bool Update(Dictionary<string, string> originalValues, Dictionary<string, string> updatedValues)
        {
            Dictionary<string, string> values = updatedValues
                                              .Where(entry => originalValues[entry.Key] != entry.Value)
                                              .ToDictionary(entry => entry.Key, entry => entry.Value);

            string attr = null;
            foreach (var item in values)
                attr += item.Key;

            return dao.Update(updatedValues, attr);
        }
        public bool Delete(int id)
        {
            return dao.Delete(id);
        }
        public List<User> Read()
        {
            return dao.GetAll();
        }
        public User GetById(int id)
        {
            return dao.GetById(id);
        }
    }
}
