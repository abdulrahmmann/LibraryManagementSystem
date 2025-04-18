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

            //## BookCopy  - BORROWREQUEST ##//
            builder.HasOne(br => br.Copy)
                .WithMany(u => u.BorrowRequests)
                .HasForeignKey(br => br.BookCopyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(br => br.BorrowedDate);
        }
    }
}
