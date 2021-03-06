﻿using Planner.Business;
using Planner.Entity;
using Planner.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.View
{
    class UserView
    {
        UserBusiness userBusiness = new UserBusiness();
        public static List<User> UsersList { get; set; }
        public static Dictionary<string, User> UsersDictionary { get; set; }
        internal void Create()
        {
            Console.WriteLine("Nome:");
            var name = Console.ReadLine();
            Console.WriteLine("Email");
            var email = Console.ReadLine();
            try
            {
                userBusiness.Create(name, email);
                UsersList = userBusiness.Read();
                Console.Clear();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        internal void Alter(int id)
        {
            User user = userBusiness.GetById(id);
            Dictionary<string, string> originalValues = GenerateDictionaries.generateUserDictionary(user.Id, user.Name, user.Email);
            Console.WriteLine("Se não hover necessidade de alteração de algum campo deixe-o vazio...");
            Console.WriteLine($"Nome atual:{user.Name}");
            Console.WriteLine("Novo nome:");
            var name = Console.ReadLine();
            Console.WriteLine($"Email atual:{user.Email}");
            Console.WriteLine("Novo email:");
            var email = Console.ReadLine();
            Dictionary<string, string> updatedValues = GenerateDictionaries.generateUserDictionary(user.Id,
                                String.IsNullOrEmpty(name) ? user.Name : name,
                                String.IsNullOrEmpty(email) ? user.Email : email);
            try
            {
                userBusiness.Update(originalValues, updatedValues);
                UsersList = userBusiness.Read();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        internal void Read()
        {
            if (UsersList == null)
                UsersList = userBusiness.Read();
            foreach (var item in UsersList)
                Console.WriteLine(item.ToString());
        }
        internal void Delete(int id)
        {
            try
            {
                userBusiness.Delete(id);
                UsersList = userBusiness.Read();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        internal void Search(string Name)
        {
            if (UsersList == null)
                UsersList = userBusiness.Read();
            if (UsersDictionary == null)
                UsersDictionary = UsersList.ToDictionary(u => u.Name, u => u);
            var results = UsersDictionary.Where(user => user.Key.Contains(Name)).Select(user => user.Value);
            foreach (var item in results)
                Console.WriteLine(item.ToString());
        }
    }
}
