using FacturacionElectronica.Configuration;
using FacturacionElectronica.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FacturacionElectronica.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComprobanteAnonimo> ComprobanteAnonimo { get; set; }
        public virtual DbSet<taComprobanteArchivo> TaComprobanteArchivo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder
                .Entity<ComprobanteAnonimo>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToTable("ComprobanteAnonimo");
                })
                .Entity<taComprobanteArchivo>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToTable("taComprobanteArchivo");
                });
        }
    }
}
