using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .HasColumnName("BookId").UseIdentityColumn();

            builder.HasIndex(b => b.Isbn).IsUnique();
            builder.HasIndex(b => b.Title);

            builder.Property(b => b.Title)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(b => b.Summary)
                .HasColumnName("Summary")
                .IsRequired()
                .HasMaxLength(800);

            builder.Property(b => b.Isbn)
                .HasColumnName("ISBN")
                .IsRequired();
            
            //## BOOK - AUTHOR -> ONE - MANY ##//
            builder
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            //## BOOK - Genre -> ONE - MANY ##//
            builder
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            //## BOOK - PUBLISHER -> ONE - MANY ##//
            builder
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            //## BOOK - PUBLISHER -> ONE - MANY ##//
            builder
                 .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}