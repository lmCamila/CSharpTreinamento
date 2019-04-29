using Planner.Business;
using Planner.Entity;
using System;
using System.Collections.Generic;

namespace Planner.View
{
    class TypePlanView
    {
        TypePlanBusiness typeBusiness = new TypePlanBusiness();

        public static List<TypePlan> TypeList { get; set; }

        internal void Create()
        {
            Console.WriteLine("Nome do tipo:");
            var name = Console.ReadLine();
            Console.WriteLine("Descrição:");
            var description = Console.ReadLine();
            var result = typeBusiness.Create(name, description);
            Console.Clear();
            if (result)
            {
                 Console.WriteLine("Tipo inserido com sucesso");
                 TypeList = typeBusiness.Read();
            }
            else
                throw new Exception("Tipo nao inserido...");
        }

        internal void Alter(int id)
        {
            TypePlan type = typeBusiness.GetById(id);
            Dictionary<string, string> originalValues = generateTypeDictionary(type.Id, type.Name,type.Description);
            Console.WriteLine("Se não hover necessidade de alteração de algum campo deixe-o vazio...");
            Console.WriteLine($"Nome atual:{type.Name}");
            Console.WriteLine("Novo nome:");
            var name = Console.ReadLine();
            Console.WriteLine($"Descrição:{type.Description}");
            Console.WriteLine("Nova descrição:");
            var description = Console.ReadLine();
            Dictionary<string, string> updatedValues = generateTypeDictionary(type.Id,
                                String.IsNullOrEmpty(name) ? type.Name : name,
                                String.IsNullOrEmpty(description) ? type.Description : description);
            if (typeBusiness.Update(originalValues, updatedValues))
            {
                Console.WriteLine("Tipo alterado com sucesso.");
                TypeList = typeBusiness.Read();
            }
            else
                throw new Exception("Tipo não pode ser alterado.");
        }
        internal Dictionary<string, string> generateTypeDictionary(long id, string name, string desc)
        {
            Dictionary<string, string> generic = new Dictionary<string, string>();
            generic.Add("id", Convert.ToString(id));
            generic.Add("name", name);
            generic.Add("desc", desc);
            return generic;
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
            var result = typeBusiness.Delete(id);
            if (result)
            {
                Console.WriteLine("Tipo excluído com sucesso");
                TypeList = typeBusiness.Read();
            }
            else
                throw new Exception("Tipo nao excluído...");
        }
    }
}
