using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Demo_many2many2.Models
{
    public class MyDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:eliasanghnaeh.database.windows.net,1433;Initial Catalog=WebbshoppGrupp8Eskilstuna;Persist Security Info=False;User ID=Group8;Password=Ourpasswordis100%secure;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public DbSet<Bokning> Bokningar { get; set; }
        public DbSet<Sällskap> Sällskaper { get; set; }


    }
}
