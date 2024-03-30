using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vou.Shared.Entities;

namespace Vou.API.EntitesConfig.Entities
{
    public class StateConfig : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(e => e.StateId);
            builder.Property(e => e.Name).HasMaxLength(100);
            //Evitar el borrado en cascada
            builder.HasOne(e => e.Country).WithMany(c => c.States).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
