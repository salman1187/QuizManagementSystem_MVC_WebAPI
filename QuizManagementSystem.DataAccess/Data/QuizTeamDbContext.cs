using QuizManagementSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagementSystem.DataAccess.Data
{
    public class QuizTeamDbContext : DbContext
    {
        public QuizTeamDbContext() : base("DefaultConnection")
        {
            
        }
        //configure tables
        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
