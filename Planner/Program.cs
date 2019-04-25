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
        static Planner planner = new Planner();
        static void Main(string[] args)
        {
            Menu();
            Console.ReadLine();
        }
        static void Menu()
        {
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("1- Planos");
            Console.WriteLine("2- Tipo de Plano");
            Console.WriteLine("3- Usuário");
            var opt = int.Parse(Console.ReadLine());
            SubMenu(opt);
        }

        static void SubMenu(int opMenu)
        {
            Console.Clear();
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("1- Cadastrar");
            Console.WriteLine("2- Ver todos");
            Console.WriteLine("3- Excluir");
            Console.WriteLine("4- Alterar");
            Console.WriteLine("5- Buscar");
            var opt = int.Parse(Console.ReadLine());
            switch (opt)
            {
                //cadastrar
                case 1 when opMenu == 1:
                    //cadastrar plano
                    
                    break;
                case 1 when opMenu == 2:
                    //cadastrar tipo
                    planner.CreateTypePlan();
                    break;
                case 1 when opMenu == 3:
                    //cadastrar user
                    
                    break;
                //listar
                case 2:
                    planner.Read(opMenu);
                    break;
                //excluir
                case 3:
                    break;
                //alterar
                case 4:
                    break;
                //buscar
                default:
                    break;
            }
        }
        


    }
}
