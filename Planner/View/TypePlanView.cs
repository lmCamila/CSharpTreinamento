using Planner.Business;
using Planner.Entity;
using Planner.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.View
{
    class TypePlanView
    {
        TypePlanBusiness typeBusiness = new TypePlanBusiness();

        public static List<TypePlan> TypeList { get; set; }
        public static Dictionary<string, TypePlan> TypeDictionary { get; set; }
        internal void Create()
        {
            Console.WriteLine("Nome do tipo:");
            var name = Console.ReadLine();
            Console.WriteLine("Descrição:");
            var description = Console.ReadLine();
            try
            {
                typeBusiness.Create(name, description);
                TypeList = typeBusiness.Read();
                Console.Clear();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal void Alter(int id)
        {
            TypePlan type = typeBusiness.GetById(id);
            Dictionary<string, string> originalValues = GenerateDictionaries.generateTypeDictionary(type.Id, type.Name,type.Description);
            Console.WriteLine("Se não hover necessidade de alteração de algum campo deixe-o vazio...");
            Console.WriteLine($"Nome atual:{type.Name}");
            Console.WriteLine("Novo nome:");
            var name = Console.ReadLine();
            Console.WriteLine($"Descrição:{type.Description}");
            Console.WriteLine("Nova descrição:");
            var description = Console.ReadLine();
            Dictionary<string, string> updatedValues = GenerateDictionaries.generateTypeDictionary(type.Id,
                                String.IsNullOrEmpty(name) ? type.Name : name,
                                String.IsNullOrEmpty(description) ? type.Description : description);
            try
            {
                typeBusiness.Update(originalValues, updatedValues);
                TypeList = typeBusiness.Read();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
       
        internal void Read()
        {
            if(TypeList == null)
                TypeList = typeBusiness.Read();
            foreach (var type in TypeList)
            {
                Console.WriteLine(type.ToString());
            }
        }

        internal void Delete(int id)
        {
            try
            {
                typeBusiness.Delete(id);
                TypeList = typeBusiness.Read();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        internal void Search(string Name)
        {
            if (TypeList == null)
                TypeList = typeBusiness.Read();
            if (TypeDictionary == null)
                TypeDictionary = TypeList.ToDictionary(tp => tp.Name, tp => tp);
            var results = TypeDictionary.Where(typePlan => typePlan.Key.Contains(Name)).Select(typePlan => typePlan.Value);
            foreach (var item in results)
                Console.WriteLine(item.ToString());
        }
    }
}
