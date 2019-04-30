using System;
using System.Collections.Generic;

namespace Planner.Entity
{
    class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Sponsor { get; set; }
        public TypePlan Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public List<User> Interested { get; set; }
        
        public override string ToString()
        {
            string interested = null;
           if(Interested != null)
            {
                foreach (var item in Interested)
                {
                    interested += $"{item.Id} - {item.Name}\n";
                }
            }
            return $"Id:{Id}\nNome:{Name}\n" +
                $"Responsável:{Sponsor.Name}\n" +
                $"Tipo:{Type.Name}" +
                $"Custos:{Cost}" +
                $"Data Inicial:{Start.ToString("dd/MM/yyyy")}\n" +
                $"Data Final:{End.ToString("dd/MM/yyyy")}" +
                $"Pessoas Interressadas:\n {interested}";
        }

    }
}
