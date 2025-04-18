using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                   .HasColumnName("GenreId")
                   .UseIdentityColumn();

            builder.Property(g => g.Name)
                   .HasColumnName("GenreName");

            builder.Property(g => g.Description)
                   .HasColumnName("GenreDescription");

            builder.Property(g => g.AverageRating)
                   .HasColumnName("GenreRating");

            builder.HasIndex(g => g.Name).IsUnique();
        }
    }
}
