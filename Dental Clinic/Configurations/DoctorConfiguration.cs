using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Email).HasMaxLength(100);
        builder.Property(d => d.PhoneNumber).HasMaxLength(15);
        builder.Property(d => d.LicenseNumber).IsRequired().HasMaxLength(50);

        // تحديد العلاقة مع جدول الاختصاصات
        builder.HasOne(d => d.Specialization)
               .WithMany(s => s.Doctors)
               .HasForeignKey(d => d.SpecializationId);
               
    }
}


