using POWERForum.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public string BlogCreator { get; set; }
        public string Title { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public Blog()
        {
            Messages = new HashSet<Message>();
        }
    }
}
