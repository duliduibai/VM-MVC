using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;

namespace VM.Repository
{
    public class VmDbContext : DbContext
    {
        public VmDbContext() //: base("name=VM")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<VmDbContext>());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountLine> AccountLines { get; set; }
    }
}
