using Planner.Business;
using Planner.Entity;
using Planner.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.View
{
    class PlanView
    {
        PlanBusiness planBusiness = new PlanBusiness();
        public static List<Plan> PlanList { get; set; }
        public static Dictionary<string, Plan> PlansDictionary { get; set; }
        public static Dictionary<string, Plan> PlansSponsorDictionary { get; set; }
        internal void Create()
        {
            Console.WriteLine("Nome do plano:");
            var name = Console.ReadLine();
            Console.WriteLine("Id do Responsavel:");
            var idSponsor = Console.ReadLine();
            Console.WriteLine("Id do Tipo:");
            var idType = Console.ReadLine();
            Console.WriteLine("Id das pessoas interessadas(separado por virgula):");
            var idInterested = Console.ReadLine();
            Console.WriteLine("Descrição do plano");
            var description = Console.ReadLine();
            Console.WriteLine("Custo(R$):");
            var cost = Console.ReadLine();
            Console.WriteLine("Data de inicio(ex: 04/04/2018):");
            var start = Console.ReadLine();
            Console.WriteLine("Data de termino(ex: 04/04/2018):");
            var end = Console.ReadLine();
            double value = String.IsNullOrEmpty(cost) ? 0 : Convert.ToDouble(cost);
            bool result;
            if(!String.IsNullOrEmpty(start)|| !String.IsNullOrEmpty(end))
            {
                result = planBusiness.Create(name, Convert.ToInt32(idSponsor), Convert.ToInt32(idType), idInterested,
                    Convert.ToDateTime(start), Convert.ToDateTime(end), description, value) ;
            }
            else
            {
                result = planBusiness.Create(name, Convert.ToInt32(idSponsor), Convert.ToInt32(idType), idInterested,
                    description,value);
            }
            if (result)
            {
                Console.WriteLine("Plano inserido com sucesso");
                PlanList = planBusiness.Read();
            }
            else
                throw new Exception("Plano não pode ser inserido");
        }

        internal void Alter(int id)
        {
            Plan p = planBusiness.GetById(id);
            var interestedPeople = p.Interested != null ? string.Join(",", p.Interested.Select(plan => plan.Id)) : "";
            Dictionary<string, string> originalValues = GenerateDictionaries.generatePlanDictionary(p.Id, p.Name, p.Sponsor.Id, 
            p.Type.Id,interestedPeople, p.Description, p.Cost, Convert.ToString(p.StartDate), Convert.ToString(p.EndDate));
            Console.WriteLine("Se não hover necessidade de mudança em algum campo deixe-o vazio...");
            Console.WriteLine("Informações atuais : ");
            Console.WriteLine(p.ToString());
            Console.WriteLine("Novo nome:");
            var name = Console.ReadLine();
            Console.WriteLine("Id do novo Responsavel:");
            var idSponsor = Console.ReadLine();
            Console.WriteLine("Id do novo Tipo:");
            var idType = Console.ReadLine();
            Console.WriteLine("Id das pessoas interessadas(separado por virgula):");
            var idInterested = Console.ReadLine();
            Console.WriteLine("Nova descrição:");
            var description = Console.ReadLine();
            Console.WriteLine("Custo(R$):");
            var cost = Console.ReadLine();
            Console.WriteLine("Data de inicio(ex: 04/04/2018):");
            var start = Console.ReadLine();
            Console.WriteLine("Data de termino(ex: 04/04/2018):");
            var end = Console.ReadLine() ;
            Dictionary<string, string> updatedValues = GenerateDictionaries.generatePlanDictionary(p.Id,
                String.IsNullOrEmpty(name) ? p.Name : name, 
                String.IsNullOrEmpty(idSponsor) ? Convert.ToString(p.Sponsor.Id): idSponsor,
                String.IsNullOrEmpty(idType) ? Convert.ToString(p.Type.Id) :idType, 
                String.IsNullOrEmpty(idInterested) ? string.Join(",", p.Interested.Select(plan => plan.Id)) : idInterested,
                String.IsNullOrEmpty(description) ? p.Description : description,
                String.IsNullOrEmpty(cost) ? Convert.ToString(p.Cost) : cost, 
                String.IsNullOrEmpty(start) ? Convert.ToString(p.StartDate) : start, 
                String.IsNullOrEmpty(end) ? Convert.ToString(p.EndDate) : end);
            if(planBusiness.Update(originalValues, updatedValues))
            {
                Console.WriteLine("Plano alterado com sucesso");
                PlanList = planBusiness.Read();
            }
            else
                throw new Exception("Plano não pode ser alterado.");
  
        }
        
        internal void Read()
        {
            if (PlanList == null)
                PlanList = planBusiness.Read();
            foreach (var plan in PlanList)
            {
                Console.WriteLine(plan.ToString());
            }
        }
        internal void Delete(int id)
        {
            var result = planBusiness.Delete(id);
            if (result)
            {
                Console.WriteLine("Plano deletado com sucesso.");
                PlanList = planBusiness.Read();
            }
            else
                throw new Exception("Plano não pode ser excluído");
        }
        internal void Search(string name)
        {
            if (PlanList == null)
                PlanList = planBusiness.Read();
            if(PlansDictionary == null)
                PlansDictionary = PlanList.ToDictionary(p => p.Name, p => p);
            var results = PlansDictionary.Where(plan => plan.Key.Contains(name)).Select(plan => plan.Value);
            foreach (var item in results)
                Console.WriteLine(item.ToString());
        }
        internal void SearchBySponsor(string name)
        {
            if (PlanList == null)
                PlanList = planBusiness.Read();
            if (PlansDictionary == null)
                PlansSponsorDictionary = PlanList.ToDictionary(p => p.Sponsor.Name, p => p);
            var results = PlansSponsorDictionary.Where(plan => plan.Key.Contains(name)).Select(plan => plan.Value);
            foreach (var item in results)
                Console.WriteLine(item.ToString());
        }
    }
}
