using Planner.View;
using System;

namespace Planner
{
    class Program
    {
        static TypePlanView typeView = new TypePlanView();
        static UserView userView = new UserView();
        static PlanView planView = new PlanView();
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
            Console.WriteLine("0- Voltar ao menu anterior");
            var opt = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (opt)
            {
                case 1:
                    Create(opMenu);
                    Continue();
                    break;
                case 2:
                    Read(opMenu);
                    Continue();
                    break;
                case 3:
                    Delete(opMenu);
                    Continue();
                    break;
                case 4:
                    Alter(opMenu);
                    Continue();
                    break;
                case 5:
                    Search(opMenu);
                    Continue();
                    break;
                case 0:
                    Console.Clear();
                    Menu();
                    break;
                default:
                    break;
            }
        }
        static void Read(int optEntity)
        {
            switch (optEntity)
            {
                case 1:
                    Console.WriteLine("Lista de Planos");
                    planView.Read();
                    break;
                case 2:
                    Console.WriteLine("Lista de Tipos de Plano");
                    typeView.Read();
                    break;
                case 3:
                    Console.WriteLine("Lista de Usuários");
                    userView.Read();
                    break;
                default:
                    break;
            }
        }

        static void Delete(int optEntity)
        {
            Console.WriteLine("Insira o id do registro que deseja deletar:");
            var id = Convert.ToInt32(Console.ReadLine());
            switch (optEntity)
            {
                case 1:
                    planView.Delete(id);
                    break;
                case 2:
                    typeView.Delete(id);
                    break;
                case 3:
                    userView.Delete(id);
                    break;
                default:
                    break;
            }
        }
        static void Create(int opMenu)
        {
            switch (opMenu)
            {
                case 1:
                    planView.Create();
                    break;
                case 2:
                    typeView.Create();
                    break;
                case 3:
                    userView.Create();
                    break;
                default:
                    break;
            }
        }
        static void Alter(int opMenu)
        { 
            int id;
            switch (opMenu)
            {
               
                case 1:
                    Console.WriteLine("Insira o id do plano:");
                    id = Convert.ToInt32(Console.ReadLine());
                    planView.Alter(id);
                    break;
                case 2:
                    Console.WriteLine("Insira o id do tipo:");
                    id = Convert.ToInt32(Console.ReadLine());
                    typeView.Alter(id);
                    break;
                case 3:
                    Console.WriteLine("Insira o id do usuário:");
                    id = Convert.ToInt32(Console.ReadLine());
                    userView.Alter(id);
                    break;
                default:
                    break;
            }
        }
        static void Search(int optEntity)
        {
            string name;
            switch (optEntity)
            {
                case 1:
                    SearchInPlans();
                    Continue();
                    break;
                case 2:
                    Console.WriteLine("Digite o nome do tipo que deseja buscar:");
                    name = Console.ReadLine();
                    typeView.Search(name);
                    Continue();
                    break;
                case 3:
                    Console.WriteLine("Digite o nome do usuário que deseja buscar:");
                    name = Console.ReadLine();
                    userView.Search(name);
                    Continue();
                    break;
                default:
                    break;
            }
        }
        static void SearchInPlans()
        {
            Console.WriteLine("Deseja buscar por:");
            Console.WriteLine("1- Nome do plano");
            Console.WriteLine("2- Nome do usuário responsável pelo plano");
            Console.WriteLine("0- Voltar para o menu inicial:");
            string name;
            var op = Convert.ToInt32(Console.ReadLine());
            switch (op)
            {
                case 1:
                    Console.WriteLine("Digite o nome do plano que deseja buscar:");
                    name = Console.ReadLine();
                    planView.Search(name);
                    break;
                case 2:
                    Console.WriteLine("Digite o nome do usuário em que deseja buscar os planos:");
                    name = Console.ReadLine();
                    planView.SearchBySponsor(name);
                    break;
                default:
                    break;
            }
        }
        static void Continue()
        {
            Console.WriteLine("Deseja voltar para o menu ? 1-sim 0-não");
            var op = Convert.ToInt32(Console.ReadLine());
            switch (op)
            {
                case 1:
                    Console.Clear();
                    Menu();
                    break;
                default:
                    break;
            }
        }
    }
}
