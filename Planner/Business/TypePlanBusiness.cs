using Planner.DAO;
using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business
{
    class TypePlanBusiness
    {
        TypePlanDAO dao = new TypePlanDAO();
        public bool Create(string name , string description)
        {
            if(String.IsNullOrEmpty(name))
            {
                throw new Exception("Nome não pode ser nulo");
            }
            TypePlan type = new TypePlan();
            type.Name = name;
            type.Description = description;
            var result = dao.Insert(type);
            return result;
        }

        public bool Update(Dictionary<string, string> originalValues, Dictionary<string, string> updatedValues)
        {
            Dictionary<string, string> values = updatedValues
                                                .Where(entry => originalValues[entry.Key] != entry.Value)
                                                .ToDictionary(entry => entry.Key, entry => entry.Value);

            string attr = null;
            //update sob demanda aqui
            foreach (var item in values)
            {
                attr += item.Key;
            }

            return dao.Update(updatedValues, attr);

        }
        public bool Delete(int id)
        {
            var result = dao.Delete(id);
            return result;
        }

        public List<TypePlan> Read()
        {
            List<TypePlan> t = dao.getAll();
            return t;
        }

        public TypePlan GetById(int id)
        {
            return dao.GetForId(id);
        }
        //usar dictionary para verificar se o id existe no delete
        //usar dictionary para buscar um tipo caso a lista ja esteja carregada
        //muda opcao de busca 

    }
}
