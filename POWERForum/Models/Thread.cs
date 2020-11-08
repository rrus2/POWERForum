using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Models
{
    public class Thread
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Blog> Blog { get; set; }
    }
}
