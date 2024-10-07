using Microsoft.EntityFrameworkCore;
using MovimientosNTT.Models;

namespace MovimientosNTT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Movimientos> Movimientos { get; set; }
        //public DbSet<Cliente> Cliente { get; set; }
    }
}
