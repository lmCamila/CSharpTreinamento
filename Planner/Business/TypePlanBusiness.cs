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
        public void Create(string name , string description)
        {
            if(String.IsNullOrEmpty(name))
            {
                throw new Exception("Nome não pode ser nulo");
            }
            TypePlan type = new TypePlan();
            type.Name = name;
            type.Description = description;
            dao.Insert(type);
        }

        public void Update(TypePlan type, string name, string description)
        {
            if (!String.IsNullOrEmpty(name))
            {
                if (!type.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) ||
                    !type.Description.Equals(description, StringComparison.InvariantCultureIgnoreCase))
                {
                    type.Name = name;
                    type.Description = description;
                    dao.Update(type);
                }
            }
        }
        public void delete(int id)
        {
             

        }

        public void ReadAll()
        {
            List<TypePlan> t = dao.getAll();
            foreach (var type in t)
            {
                Console.WriteLine(type.ToString());
            }
        }
        //usar dictionary para verificar se o id existe no delete
        //usar dictionary para buscar um tipo caso a lista ja esteja carregada
        //muda opcao de busca 

    }
}
