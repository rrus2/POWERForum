using Microsoft.AspNetCore.Identity;
using POWERForum.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Models
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
