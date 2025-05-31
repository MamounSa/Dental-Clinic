using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.HasKey(mr => mr.Id);
        builder.Property(mr => mr.RecordDate).IsRequired();
        builder.Property(mr => mr.Diagnosis).IsRequired().HasMaxLength(500);
        builder.Property(mr => mr.Medications).HasMaxLength(500);
        builder.Property(mr => mr.Notes).HasMaxLength(1000);

        builder.HasOne(mr => mr.Patient)
               .WithOne(p => p.MedicalRecords)
               .HasForeignKey<MedicalRecord>(p=>p.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(mr => mr.Images)
               .WithOne(img => img.MedicalRecord)
               .HasForeignKey(img => img.MedicalRecordId)
               .OnDelete(DeleteBehavior.Cascade); // ✅ عند حذف السجل، يتم حذف الصور
    }
}
