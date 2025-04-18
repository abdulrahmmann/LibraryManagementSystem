using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<Review> BookReview { get; set; }
        public virtual DbSet<Copy> Copy { get; set; }
        public virtual DbSet<BorrowRequest> BorrowRequest { get; set; }
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
