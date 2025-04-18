using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasColumnName("AuthorId")
            .UseIdentityColumn();

            builder.HasIndex(b => b.Name);

            builder.Property(a => a.Name).HasColumnName("AuthorName").HasMaxLength(30);

            builder.Property(a => a.Biography).HasColumnName("AuthorBiography").HasMaxLength(300);

            builder.Property(a => a.Nationality).HasColumnName("AuthorNationality");

            builder.Property(a => a.BirthDate).HasColumnName("AuthorBirthDate");

            builder.HasIndex(a => a.Name);
        }
    }
}
