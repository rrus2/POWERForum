using POWERForum.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime DateTime { get; set; }
    }
}
