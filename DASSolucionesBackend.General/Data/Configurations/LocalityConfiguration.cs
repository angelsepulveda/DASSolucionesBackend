using DASSolucionesBackend.General.Submodules.Address.Localities.Entities;

namespace DASSolucionesBackend.General.Data.Configurations;

public class LocalityConfiguration : IEntityTypeConfiguration<Locality>
{
    public void Configure(EntityTypeBuilder<Locality> builder)
    {
        builder.ToTable("localities");

        builder.HasKey(c => c.Id);

        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();

        builder.Property(c => c.CountryId).IsRequired();
        
        builder.Property(c => c.RegionId).IsRequired();

        builder.Property(p => p.Code).HasMaxLength(50);

        builder.Property(p => p.Status).IsRequired();

        //relaciones
        builder.HasOne(u => u.Country).WithOne().HasForeignKey<Locality>("CountryId");
        builder.HasOne(u => u.Region).WithOne().HasForeignKey<Locality>("RegionId");
    }
}