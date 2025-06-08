using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DentalModelConfiguration : IEntityTypeConfiguration<DentalModel>
{
    public void Configure(EntityTypeBuilder<DentalModel> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.ToothNumber)
            .IsRequired();

        builder.Property(d => d.Condition)
            .HasMaxLength(100);

        builder.HasOne(d => d.Patient)
            .WithMany(p => p.DentalModels)
            .HasForeignKey(d => d.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.MedicalRecord)
            .WithMany(m => m.DentalModels)
            .HasForeignKey(d => d.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Treatments)
            .WithOne(t => t.DentalModel)
            .HasForeignKey(t => t.DentalModelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
