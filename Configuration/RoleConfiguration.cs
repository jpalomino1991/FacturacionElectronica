using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacturacionElectronica.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Anonimo",
                    NormalizedName = "ANONIMO"
                },
                new IdentityRole
                {
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR"
                }
            );
        }
    }
}
