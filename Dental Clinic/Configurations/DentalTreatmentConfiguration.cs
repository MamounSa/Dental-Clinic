using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DentalTreatmentConfiguration : IEntityTypeConfiguration<DentalTreatment>
{
    public void Configure(EntityTypeBuilder<DentalTreatment> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TreatmentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.TreatmentDate)
            .IsRequired();

        builder.Property(t => t.Notes)
            .HasMaxLength(250);

        builder.HasOne(t => t.DentalModel)
            .WithMany(d => d.Treatments)
            .HasForeignKey(t => t.DentalModelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Doctor)
            .WithMany()
            .HasForeignKey(t => t.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
