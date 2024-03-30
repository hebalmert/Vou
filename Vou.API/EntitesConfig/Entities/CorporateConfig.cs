using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vou.Shared.Entities;

namespace Vou.API.EntitesConfig.Entities
{
    public class CorporateConfig : IEntityTypeConfiguration<Corporate>
    {
        public void Configure(EntityTypeBuilder<Corporate> builder)
        {
            builder.HasKey(e => e.CorporateId);
            builder.HasIndex("Name").IsUnique();
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.ImageId).HasMaxLength(256);
            builder.Property(e => e.Document).HasMaxLength(25);
            builder.Property(e => e.ToStar).HasColumnType("date");
            builder.Property(e => e.ToEnd).HasColumnType("date");
            //Evitar el borrado en cascada
            builder.HasOne(e => e.SoftPlan).WithMany(c => c.Corporates).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(e => e.Country).WithMany(c => c.Corporates).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(e => e.State).WithMany(c => c.Corporates).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(e => e.City).WithMany(c => c.Corporates).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
