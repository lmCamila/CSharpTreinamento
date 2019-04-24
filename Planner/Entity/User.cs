using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Entity
{
    class User
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public User(long id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
    }
}
