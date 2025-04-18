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

            builder.Property(br => br.Id)
                .HasColumnName("BookReviewId")
                .UseIdentityColumn();

            builder.Property(br => br.Title)
                .HasColumnName("ReviewTitle")
                .HasMaxLength(30);

            builder.Property(br => br.Name)
                .HasColumnName("ReviewerName")
                .HasMaxLength(30);


            builder.Property(br => br.Comment)
                .HasColumnName("ReviewerComment")
                .HasMaxLength(30);


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
