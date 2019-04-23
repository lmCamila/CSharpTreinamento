using Planner.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner
{
    class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User(1, "Camila de Lima Martins", "camilalm31@gmail.com");
            TypePlan tipo1 = new TypePlan(1,"Plano estratégico","Plano para orientar estratégias.");
            Plan p = new Plan(1, "Plano 1", user1, tipo1, new DateTime(2019, 04, 05), new DateTime(2019, 05, 31));

            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
