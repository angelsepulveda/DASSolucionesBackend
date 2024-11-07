using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Data.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("regions");

        builder.HasKey(c => c.Id);

        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();

        builder.Property(c => c.CountryId).IsRequired();

        builder.Property(p => p.Code).HasMaxLength(50);

        builder.Property(p => p.Status).IsRequired();

        //relaciones
        builder.HasOne(u => u.Country).WithOne().HasForeignKey<Region>("CountryId");
    }
}