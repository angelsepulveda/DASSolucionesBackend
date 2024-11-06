using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Code).HasMaxLength(50);
        builder.Property(p => p.Demonym).HasMaxLength(255);
        builder.Property(p => p.Status).IsRequired();
    }
}