using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Models
{
    public class Thread
    {
        public int ThreadID { get; set; }
        public string ThreadCreator { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public Thread()
        {
            Blogs = new HashSet<Blog>();
        }
    }
}
