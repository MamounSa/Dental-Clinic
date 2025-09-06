using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(i => i.IssueDate).IsRequired();
        builder.Property(i => i.DueDate).IsRequired();

        builder.Property(i => i.Status)
            .HasConversion<string>() // لتخزين الenum كنص
            .IsRequired();

        builder.HasOne(i => i.Patient)
            .WithMany(p => p.Invoices)
            .HasForeignKey(i => i.PatientId);

        builder.HasOne(i => i.Appointment)
            .WithOne(a => a.Invoice)
            .HasForeignKey<Invoice>(i => i.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(i => i.Payments)
            .WithOne(p => p.Invoice)
            .HasForeignKey(p => p.InvoiceId);
    }
}