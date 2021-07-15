using System;
using University.NetStandart.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace University.NetStandart.DAL.DbContext
{
    public class  ApplicationDbContext : IdentityDbContext<User, Role, int>

    {
       public DbSet<Students> Students { get; set; }
       public DbSet<Lessons> Lessons { get; set; }
       public DbSet<StudentList> StudentList { get; set; }
       public DbSet<Report1> Report1 { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

       
    }
    
    
}
