using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Entity
{
    class TypePlan
    {
        public TypePlan() { }

        public TypePlan(long id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public long Id  { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Id}- {Name} - {Description}";
        }
    }
}
