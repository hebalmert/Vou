using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vou.Shared.Entities;

namespace Vou.API.EntitesConfig.Entities
{
    public class ManagerConfig : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(e => e.ManagerId);
            builder.HasIndex("UserName").IsUnique();
            builder.Property(e => e.Document).HasMaxLength(25);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.FullName).HasMaxLength(100);
            builder.Property(e => e.Photo).HasMaxLength(256);
            builder.Property(e => e.PhoneNumber).HasMaxLength(25);
            builder.Property(e => e.Address).HasMaxLength(256);
            builder.Property(e => e.UserName).HasMaxLength(256);
            //Evitar el borrado en cascada
            builder.HasOne(e => e.Corporate).WithMany(c => c.Managers).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
