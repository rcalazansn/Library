using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Mappings
{
    public class LoanMap : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder
              .ToTable("loans");

            builder
                .HasKey(t => t.Id);

            builder
                .HasOne(t => t.User)
                .WithMany(t => t.Loans)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Book)
                .WithMany(t => t.Loans)
                .HasForeignKey(t => t.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
