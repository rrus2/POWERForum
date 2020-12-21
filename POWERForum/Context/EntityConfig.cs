using Microsoft.EntityFrameworkCore;
using POWERForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace POWERForum.Context
{
    public class EntityConfig
    {
        public EntityConfig(ModelBuilder builder)
        {
            //thread config
            builder.Entity<Thread>().HasKey(x => x.ThreadID);
            builder.Entity<Thread>().Property(x => x.ThreadCreator).IsRequired();
            builder.Entity<Thread>().Property(x => x.Name).IsRequired();
            builder.Entity<Thread>().Property(x => x.Type).IsRequired();
            builder.Entity<Thread>().HasMany(x => x.Blogs).WithOne().HasForeignKey(x => x.BlogID);

            //blog config
            builder.Entity<Blog>().HasKey(x => x.BlogID);
            builder.Entity<Blog>().Property(x => x.BlogCreator).IsRequired();
            builder.Entity<Blog>().Property(x => x.Title).IsRequired();
            builder.Entity<Blog>().HasMany(x => x.Messages).WithOne().HasForeignKey(x => x.MessageID);

            //user config
            builder.Entity<ApplicationUser>().HasKey(x => x.Id);
            builder.Entity<ApplicationUser>().HasMany(x => x.Messages).WithOne(x => x.ApplicationUser);
            builder.Entity<ApplicationUser>().Property(x => x.Birthdate);

            //mesasge config
            builder.Entity<Message>().HasKey(x => x.MessageID);
            builder.Entity<Message>().Property(x => x.MessageText).IsRequired();
            builder.Entity<Message>().Property(x => x.DateTime).IsRequired();
            builder.Entity<Message>().HasOne(x => x.ApplicationUser);
        }
    }
}
