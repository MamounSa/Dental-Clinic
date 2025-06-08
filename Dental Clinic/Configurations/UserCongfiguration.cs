using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.DateofBirth).IsRequired();
        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.Property(u => u.PasswordSalt)
               .IsRequired();

        builder.Property(u => u.Role)
               .HasDefaultValue("User");
    }
}