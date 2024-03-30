using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vou.Shared.Entities;

namespace Vou.API.EntitesConfig.Entities
{
    public class SoftPlanConfig : IEntityTypeConfiguration<SoftPlan>
    {
        public void Configure(EntityTypeBuilder<SoftPlan> builder)
        {
            builder.HasKey(e => e.SoftPlanId);
            builder.HasIndex("Name").IsUnique();
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Price).HasPrecision(18, 2);
        }

    }
}
