using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Model
{
    public class User
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

        internal User(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
