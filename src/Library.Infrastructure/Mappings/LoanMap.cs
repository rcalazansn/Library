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
                .HasKey(_ => _.Id);

            builder
                .HasOne(_ => _.User)
                .WithMany(_ => _.Loans)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(_ => _.Book)
                .WithMany(_ => _.Loans)
                .HasForeignKey(_ => _.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
