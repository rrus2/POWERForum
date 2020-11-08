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
            builder.Entity<Thread>().HasKey(x => x.ID);
            builder.Entity<Thread>().Property(x => x.Name).IsRequired();
            builder.Entity<Thread>().Property(x => x.Type).IsRequired();
            builder.Entity<Thread>().HasMany(x => x.Blog).WithOne().OnDelete(DeleteBehavior.Cascade);

            //blog config
            builder.Entity<Blog>().HasKey(x => x.ID);
            builder.Entity<Blog>().Property(x => x.Message).IsRequired();
            builder.Entity<Blog>().Property(x => x.Title).IsRequired();
            builder.Entity<Blog>().HasOne(x => x.ApplicationUser);

            //user config
            builder.Entity<ApplicationUser>().HasKey(x => x.Id);
            builder.Entity<ApplicationUser>().HasMany(x => x.Blogs).WithOne();
            builder.Entity<ApplicationUser>().Property(x => x.Birthdate).IsRequired();
        }
    }
}
