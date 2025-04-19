using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class BorrowRequestConfiguration : IEntityTypeConfiguration<BorrowRequest>
    {
        public void Configure(EntityTypeBuilder<BorrowRequest> builder)
        {
            builder.ToTable("BorrowRequest");

            builder.HasKey(br => br.Id);

            builder.Property(br => br.Id).HasColumnName("BorrowRequestId")
                .UseIdentityColumn();

            //## USER  - BORROWREQUEST ##//
            builder.HasOne(br => br.User)
                .WithMany(u => u.BorrowRequests)
                .HasForeignKey(br => br.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //## BOOK  - BORROWREQUEST ##//
            builder.HasOne(br => br.Book)
                .WithMany(u => u.BorrowRequest)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(br => br.BorrowedDate);
        }
    }
}
