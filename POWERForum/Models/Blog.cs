using POWERForum.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
