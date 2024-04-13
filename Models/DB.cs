using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kosarHospital.Models
{
    public class DB : IdentityDbContext<User>
    {
        public DB(DbContextOptions<DB> option) : base(option)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hospital;multipleactiveresultsets=True;Integrated Security=true;Encrypt=False;Trust Server Certificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Time> Time { get; set; }
    }
}