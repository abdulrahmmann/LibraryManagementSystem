using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("UserId")
                   .UseIdentityColumn();

            builder.Property(u => u.UserName)
                   .HasColumnName("UserName");

            builder.Property(u => u.Email)
                   .HasColumnName("UserEmail");

            builder.Property(u => u.PhoneNumber)
                   .HasColumnName("UserPhoneNumber");

            builder.Property(u => u.BirthDate)
                   .HasColumnName("UserBirthDate");

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasIndex(u => u.PhoneNumber).IsUnique();
        }
    }
}
