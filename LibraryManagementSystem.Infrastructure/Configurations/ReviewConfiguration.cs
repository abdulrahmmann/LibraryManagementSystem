using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("BookReviews");

            builder.HasKey(br => br.Id);

            builder.Property(br => br.Id).HasColumnName("BookReviewId")
            .UseIdentityColumn();

            builder.Property(br => br.Title).HasColumnName("BookReviewTitle");
            builder.Property(br => br.Name).HasColumnName("BookReviewerName");

            //## BOOKREVIEW - BOOK -> ONE - MANY ##//
            builder
                    .HasOne(br => br.Book)
                .WithMany(b => b.BookReviews)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(br => br.Title);
        }
    }
}
