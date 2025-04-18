using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class CopyConfiguration : IEntityTypeConfiguration<Copy>
    {
        public void Configure(EntityTypeBuilder<Copy> builder)
        {
            builder.ToTable("BookCopies");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnName("CopyId").UseIdentityColumn();

            //## BOOK - COPY -> ONE - MANY ##//
            builder
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
