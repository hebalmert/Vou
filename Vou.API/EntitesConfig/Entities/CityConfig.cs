using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vou.Shared.Entities;

namespace Vou.API.EntitesConfig.Entities
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.CityId);
            builder.Property(e => e.Name).HasMaxLength(100);
            //Evitar el borrado en cascada
            builder.HasOne(e => e.State).WithMany(c => c.Cities).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
