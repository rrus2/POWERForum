using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Models
{
    public class UserToReturn
    {
        public string Email { get; set; }
        public IEnumerable<int> CreatedThreads { get; set; }
        public IEnumerable<int> CreatedBlogs { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
