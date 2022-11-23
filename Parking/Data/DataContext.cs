using Microsoft.EntityFrameworkCore;
using Parking.Data.Entities;

namespace Parking.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        //
        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet <Cliente> Clientes { get; set; }

        public DbSet<ClientePlan> ClientesPlanes { get; set; }

        public DbSet<Plan> Planes { get; set; }
        
        public DbSet<Parqueadero> Parqueaderos { get; set; }

        public DbSet<RegistroEntrada> RegistroEntradas { get; set; }
        public DbSet<Recibo> Recibos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehiculo>().HasIndex("Placa","ClienteId" ).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(c=>c.Cedula).IsUnique();
            modelBuilder.Entity<Plan>().HasIndex(p=>p.Nombre).IsUnique();
            modelBuilder.Entity<Recibo>().HasIndex(r => r.Id).IsUnique();


        }

    }
}
