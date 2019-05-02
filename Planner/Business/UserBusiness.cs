using Planner.DAO;
using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Business
{
    class UserBusiness
    {
        UserDAO dao = new UserDAO();
        public void Create(string name, string email)
        {
            if(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(email))
                throw new Exception("Nome ou email não pode ser nulo");

            User user = new User();
            user.Name = name;
            user.Email = email;
            var result = dao.Insert(user);
            if (result)
                Console.WriteLine("Usuário inserido com sucesso");
            else
                throw new Exception("usuário não pode ser inserido");
        }

        public void Update(Dictionary<string, string> originalValues, Dictionary<string, string> updatedValues)
        {
            Dictionary<string, string> values = updatedValues
                                              .Where(entry => originalValues[entry.Key] != entry.Value)
                                              .ToDictionary(entry => entry.Key, entry => entry.Value);

            string attr = null;
            foreach (var item in values)
                attr += item.Key;

            var result = dao.Update(updatedValues, attr);
            if (result)
                Console.WriteLine("Usuário alterado com sucesso.");
            else
                throw new Exception($"Não foi possível alterar o usuário {originalValues["name"]}");
        }
        public void Delete(int id)
        {
            var result = dao.Delete(id);
            if (result)
                Console.WriteLine("Usuário excluído com sucesso.");
            else
                throw new Exception("Usuário não excluído...");
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
