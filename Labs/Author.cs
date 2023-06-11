using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author
{
    class Author
    {
        public int Id { get; }
        public string Name { get; }
        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
