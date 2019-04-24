using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner
{
    class Planner
    {
        internal void CreateUser()
        {
            Console.WriteLine("Nome:");
            var name = Console.ReadLine();
            Console.WriteLine("Email");
            var email = Console.ReadLine();
            //inserir no banco pegar retorno e colocar em um novo usuario
            User user = new User(1, name, email);
        }
        internal void CreatePlan()
        {

        }
        internal void CreateTypePlan()
        {

        }
    }
}
