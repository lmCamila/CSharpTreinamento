﻿
namespace Planner.Entity
{
    class User
    {
     
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public override string ToString()
        {
            return $"{Id}-{Name},{Email}";
        }
    }
}
