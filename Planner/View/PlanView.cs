using Planner.Business;
using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.View
{
    class PlanView
    {
        PlanBusiness planBusiness = new PlanBusiness();
        public static List<Plan> PlanList { get; set; }
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
            Dictionary<string, string> originalValues = generatePlanDictionary(p.Id,p.Name,p.Sponsor.Id,p.Type.Id,
                string.Join(",",p.Interested.Select(plan => plan.Id)),p.Description,p.Cost, p.Start.ToString(), p.End.ToString());
            Console.WriteLine("Se não hover necessidade de mudança em algum campo deixe-o vazio...");
            Console.WriteLine("Nome Atual:");
            Console.WriteLine("Novo nome:");
            var name = Console.ReadLine();
            Console.WriteLine("Id do Responsavel Atual:");
            Console.WriteLine("Id do novo Responsavel:");
            var idSponsor = Console.ReadLine();
            Console.WriteLine("Id do Tipo Atual:");
            Console.WriteLine("Id do novo Tipo:");
            var idType = Console.ReadLine();
            Console.WriteLine("Id das pessoas interessadas");
            Console.WriteLine("Id das pessoas interessadas(separado por virgula):");
            var idInterested = Console.ReadLine();
            Console.WriteLine("Descrição do plano:");
            Console.WriteLine("Nova descrição:");
            var description = Console.ReadLine();
            Console.WriteLine("Custo atual: R$");
            Console.WriteLine("Custo(R$):");
            var cost = Console.ReadLine();
            Console.WriteLine("Data de inicio(ex: 04/04/2018):");
            Console.WriteLine("Inicio em:");
            var start = Console.ReadLine();
            Console.WriteLine("Data de termino(ex: 04/04/2018):");
            Console.WriteLine("Termino em:");
            var end = Console.ReadLine();
            Dictionary<string, string> updatedValues = generatePlanDictionary(p.Id, name, Convert.ToInt64(idSponsor),
                Convert.ToInt64(idType), idInterested, description, Convert.ToDouble(cost), start, end);
            if(planBusiness.Update(originalValues, updatedValues))
            {
                Console.WriteLine("Plano alterado com sucesso");
            }
            else
                throw new Exception("Plano não pode ser alterado.");
            

        }
        internal Dictionary<string, string> generatePlanDictionary(int id, string name, long idSponsor, long idType,
            string idsInterested, string description, double costs, string startDate, string endDate)
        {
            Dictionary<string, string> generic = new Dictionary<string, string>();
            generic.Add("id", Convert.ToString(id));
            generic.Add("name", name);
            generic.Add("idSponsor", Convert.ToString(idSponsor));
            generic.Add("idType", Convert.ToString(idType));
            generic.Add("idsInterested",idsInterested);
            generic.Add("description",description);
            generic.Add("costs", Convert.ToString(costs));
            generic.Add("start",startDate);
            generic.Add("end",endDate);
            return generic;
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
    }
}
