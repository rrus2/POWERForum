using Microsoft.AspNetCore.Identity;
using POWERForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Context
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public ApplicationUser()
        {
            Blogs = new HashSet<Blog>();
        }
    }
}
