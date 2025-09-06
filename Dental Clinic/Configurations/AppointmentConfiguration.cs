using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Start).IsRequired();
        builder.Property(a => a.End).IsRequired();

        builder.Property(a => a.Status).IsRequired().HasMaxLength(50);
        builder.Property(a => a.AttendanceStatus).IsRequired(false).HasMaxLength(50);
        builder.Property(a => a.CheckInTime).IsRequired(false);
        builder.Property(a => a.CheckOutTime).IsRequired(false);
        builder.HasOne(a => a.Doctor)
               .WithMany(d => d.Appointments)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Restrict); // 🔹 منع حذف الطبيب إذا كانت هناك مواعيد مرتبطة به

        builder.HasOne(a => a.Patient)
               .WithMany(p => p.Appointments)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Restrict); // 🔹 منع حذف المريض إذا كانت هناك مواعيد مرتبطة به
    }
}
