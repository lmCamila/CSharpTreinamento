using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.View
{
    class UserView
    {
        internal void CreateUser()
        {
            Console.WriteLine("Nome:");
            var name = Console.ReadLine();
            Console.WriteLine("Email");
            var email = Console.ReadLine();

        }
        internal void AlterUser()
        {
            Console.WriteLine("Se não hover necessidade de alteração de algum campo deixe-o vazio...");
            Console.WriteLine("Nome atual:");
            Console.WriteLine("Novo nome:");
            var name = Console.ReadLine();
            Console.WriteLine("Email atual:");
            Console.WriteLine("Novo email:");
            var email = Console.ReadLine();
        }


    }
}
