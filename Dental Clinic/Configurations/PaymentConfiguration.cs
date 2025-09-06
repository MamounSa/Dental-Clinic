using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Status)
               .IsRequired();

        builder.Property(p => p.PaymentDate)
               .IsRequired();

       /* builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.Property(p => p.UpdatedAt)
               .IsRequired(false);*/

        builder.Property(p => p.PaymentType)
               .IsRequired();

        builder.HasOne(p => p.Patient)
               .WithMany()
               .HasForeignKey(p => p.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Invoice)
               .WithMany(i => i.Payments)
               .HasForeignKey(p => p.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}