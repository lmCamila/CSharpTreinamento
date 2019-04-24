using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Entity
{
    class Plan
    {
        public Plan(long id, string name, User sponsor, TypePlan type, DateTime start, DateTime end)
        {
            Id = id;
            Name = name;
            Sponsor = sponsor;
            Type = type;
            Start = start;
            End = end;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public User Sponsor { get; set; }
        public TypePlan Type { get; set; }
        public List<User> Interested { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}\nNome:{Name}\n" +
                $"Responsável:{Sponsor.Name}\n" +
                $"Data Inicial:{Start.ToString("dd/MM/yyyy")}\nData Final:{End.ToString("dd/MM/yyyy")}";
        }

    }
}
