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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
                $"Tipo:{Type.Name}\n" +
                $"Descrição:{Description}\n" +
                $"Custos:{Cost}\n" +
                $"Data Inicial:{StartDate.ToString("dd/MM/yyyy")}\n" +
                $"Data Final:{EndDate.ToString("dd/MM/yyyy")}\n" +
                $"Pessoas Interressadas:\n{interested}";
        }

    }
}
