using Planner.DAO;
using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Business
{
    class PlanBusiness
    {
        PlanDAO dao = new PlanDAO();
        public void Create(string name, int idSponsor, int idTypePlan, string idsInterested, string description = null, 
            double cost = 0)
        {
            Plan plan = new Plan();
            plan.Name = name;
            plan.Type = new TypePlan();
            plan.Type.Id = idTypePlan;
            plan.Sponsor = new User();
            plan.Sponsor.Id = idSponsor;
            if (description != null)
                plan.Description = description;
            if (cost != 0)
                plan.Cost = cost;

            var result = dao.insert(plan, idsInterested);
            if (result)
                Console.WriteLine("Plano inserido com sucesso");
            else
                throw new Exception("Plano não pode ser inserido");
        }
        public void Create(string name, int idSponsor, int idTypePlan, string idsInterested,DateTime start, DateTime end, 
            string description = null, double cost = 0)
        {
            Plan plan = new Plan();
            plan.Name = name;
            plan.Type = new TypePlan();
            plan.Type.Id = idTypePlan;
            plan.Sponsor = new User();
            plan.Sponsor.Id = idSponsor;
            plan.StartDate = start;
            plan.EndDate = end;
            if (description != null)
                plan.Description = description;
            if (cost != 0)
                plan.Cost = cost;
            var result = dao.insert(plan, idsInterested);
            if (result)
                Console.WriteLine("Plano inserido com sucesso");
            else
                throw new Exception("Plano não pode ser inserido");
        }

        public void Update(Dictionary<string, string> originalValues, Dictionary<string, string> updatedValues)
        {
            Dictionary<string, string> values = updatedValues
                                                .Where(entry => originalValues[entry.Key] != entry.Value)
                                                .ToDictionary(entry => entry.Key, entry => entry.Value);

            string attr = null;
            foreach (var item in values)
                attr = String.Join(",", item.Key);
           
            var result = values != null ? dao.Update(updatedValues,attr) : false;
            if (result)
                Console.WriteLine("Plano alterado com sucesso");
            else
                throw new Exception("Plano não pode ser alterado.");
            
        }
        public void Delete(int id)
        {
            var result = dao.Delete(id);
            if (result)
                Console.WriteLine("Plano deletado com sucesso.");
            else
                throw new Exception("Plano não pode ser excluído");
        }

        public List<Plan> Read()
        {
            return dao.GetAll();
        }

        public Plan GetById(int id)
        {
            return dao.GetById(id);
        }
    }
}
