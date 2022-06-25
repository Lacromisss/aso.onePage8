using aspEv.Models;
using Microsoft.EntityFrameworkCore;

namespace aspEv.Dal
{
    public class AppDbContext:DbContext
    {
        public  AppDbContext(DbContextOptions options):base(options)
        {

        }
      public  DbSet<User> users { get;set; }
    }
}
