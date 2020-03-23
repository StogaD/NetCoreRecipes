using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class Blog
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
