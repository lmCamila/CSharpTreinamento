﻿using System;
namespace Planner.View
{
    class PlanView
    {
        internal void Create()
        {
            Console.WriteLine("Nome do plano:");
            var name = Console.ReadLine();
            Console.WriteLine("Id do Responsavel:");
            var idSponsor = Console.ReadLine();
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
            
           
        }

        internal void Alter()
        {
            Console.WriteLine("Se não hover necessidade de mudança em algum campo deixe-o vazio...");
            Console.WriteLine("Nome Atual:");
            Console.WriteLine("Novo nome:");
            var name = Console.ReadLine();
            Console.WriteLine("Id do Responsavel Atual:");
            Console.WriteLine("Id do novo Responsavel:");
            var idSponsor = Console.ReadLine();
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
        }


    }
}
