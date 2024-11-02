using DASSolucionesBackend.General.Submodules.DocumentTypes.Entities;

namespace DASSolucionesBackend.General.Data.Configurations;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable("document_types");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
        builder.Property(p => p.Code).HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(255);
        builder.Property(p => p.Status).IsRequired();
    }
}