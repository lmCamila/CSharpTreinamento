using Planner.DAO;
using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Business
{
    class TypePlanBusiness
    {
        TypePlanDAO dao = new TypePlanDAO();
        public void Create(string name , string description)
        {
            if(String.IsNullOrEmpty(name))
            {
                throw new Exception("Nome não pode ser nulo");
            }
            TypePlan type = new TypePlan();
            type.Name = name;
            type.Description = description;
            var result = dao.Insert(type);
            if (result)
            {
                Console.WriteLine("Tipo inserido com sucesso");
            }
            else
                throw new Exception("Tipo não pode ser inserido...");
        }

        public void Update(Dictionary<string, string> originalValues, Dictionary<string, string> updatedValues)
        {
            Dictionary<string, string> values = updatedValues
                                                .Where(entry => originalValues[entry.Key] != entry.Value)
                                                .ToDictionary(entry => entry.Key, entry => entry.Value);

            string attr = null;
            foreach (var item in values)
                attr += item.Key;
            
            var result =  dao.Update(updatedValues, attr);

            if (result)
                Console.WriteLine("Tipo alterado com sucesso");
            else
                throw new Exception("Tipo não pode ser alterado.");
        }
        public void Delete(int id)
        {
            var result = dao.Delete(id);
            if (result)
            {
                Console.WriteLine("Tipo excluído com sucesso");
            }
            else
                throw new Exception("Tipo não pode ser excluído...");
        }

        public List<TypePlan> Read()
        {
            return dao.getAll();
        }

        public TypePlan GetById(int id)
        {
            return dao.GetForId(id);
        }

    }
}
