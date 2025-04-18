using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("PublisherId")
                   .UseIdentityColumn();

            builder.Property(p => p.Name)
                   .HasColumnName("PublisherName");

            builder.Property(p => p.Email)
                   .HasColumnName("PublisherEmail");

            builder.Property(p => p.PhoneNumber)
                   .HasColumnName("PublisherPhoneNumber");

            builder.HasIndex(p => p.Email).IsUnique();

            builder.HasIndex(p => p.PhoneNumber).IsUnique();
        }
    }
}
