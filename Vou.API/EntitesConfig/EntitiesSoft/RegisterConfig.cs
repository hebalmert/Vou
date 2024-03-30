using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vou.Shared.EntitiesSoft;

namespace Vou.API.EntitesConfig.EntitiesSoft
{
    public class RegisterConfig : IEntityTypeConfiguration<Register>
    {
        public void Configure(EntityTypeBuilder<Register> builder)
        {
            builder.HasKey(e => e.RegisterId);
        }
    }
}
